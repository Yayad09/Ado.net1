using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TP1_LSDA.Repositories
{
    /// <summary>
    /// Classe de base des repositories : gère la chaîne de connexion, l'ouverture/fermeture
    /// des connexions et factorise le code ADO.NET commun aux modes connecté et déconnecté.
    /// </summary>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected RepositoryBase(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("La chaîne de connexion est obligatoire.", nameof(connectionString));

            ConnectionString = connectionString;
        }

        protected string ConnectionString { get; }

        /// <summary>Nom de la table SQL (ex. « Lieu »). Sert aussi de nom de DataTable dans le DataSet.</summary>
        public abstract string TableName { get; }

        /// <summary>
        /// SELECT mono-table utilisé par le SqlDataAdapter. Il doit contenir la clé primaire
        /// pour que le SqlCommandBuilder puisse générer les commandes INSERT/UPDATE/DELETE.
        /// </summary>
        protected abstract string SelectAllSql { get; }

        /// <summary>Transforme la ligne courante d'un SqlDataReader en entité.</summary>
        protected abstract T Map(SqlDataReader reader);

        // =====================================================================
        //  Gestion de la connexion
        // =====================================================================

        protected SqlConnection CreateConnection() => new SqlConnection(ConnectionString);

        protected static SqlParameter Param(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type) { Value = value ?? DBNull.Value };
        }

        protected static SqlParameter Param(string name, SqlDbType type, int size, object value)
        {
            return new SqlParameter(name, type, size) { Value = value ?? DBNull.Value };
        }

        // =====================================================================
        //  Mode connecté (SqlConnection / SqlCommand / SqlParameter / SqlDataReader)
        // =====================================================================

        /// <summary>Exécute une requête paramétrée et lit les résultats séquentiellement avec un SqlDataReader.</summary>
        protected List<TResult> ExecuteReader<TResult>(string sql, Func<SqlDataReader, TResult> map,
                                                       params SqlParameter[] parameters)
        {
            var results = new List<TResult>();

            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(map(reader));
                    }
                }
            }

            return results;
        }

        protected List<T> ExecuteReader(string sql, params SqlParameter[] parameters)
        {
            return ExecuteReader(sql, Map, parameters);
        }

        protected int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public virtual List<T> GetAll()
        {
            return ExecuteReader(SelectAllSql + " ORDER BY Id;");
        }

        public virtual T GetById(int id)
        {
            string sql = $"SELECT * FROM ({SelectAllSql}) AS t WHERE t.Id = @Id;";
            List<T> list = ExecuteReader(sql, Param("@Id", SqlDbType.Int, id));
            return list.Count > 0 ? list[0] : null;
        }

        public abstract int Add(T entity);

        public abstract bool Update(T entity);

        public virtual bool Delete(int id)
        {
            // TableName est une constante définie dans le code (jamais une saisie utilisateur).
            string sql = $"DELETE FROM dbo.[{TableName}] WHERE Id = @Id;";
            return ExecuteNonQuery(sql, Param("@Id", SqlDbType.Int, id)) > 0;
        }

        // =====================================================================
        //  Mode déconnecté (SqlDataAdapter / DataSet / DataTable / SqlCommandBuilder)
        // =====================================================================

        /// <summary>
        /// Remplit un DataSet (une DataTable nommée comme la table SQL). Le schéma (clé primaire,
        /// identité, longueurs maximales, nullabilité) est aussi chargé pour sécuriser l'édition.
        /// </summary>
        public virtual DataSet GetDataSet()
        {
            var dataSet = new DataSet("TP1_LSDA");

            using (SqlConnection connection = CreateConnection())
            using (var adapter = new SqlDataAdapter(SelectAllSql + " ORDER BY Id;", connection))
            {
                // 1) Schéma : clé primaire, colonne identité, MaxLength, AllowDBNull.
                adapter.FillSchema(dataSet, SchemaType.Source, TableName);

                DataColumn idColumn = dataSet.Tables[TableName].Columns["Id"];
                if (idColumn != null && idColumn.AutoIncrement)
                {
                    // Identifiants temporaires négatifs pour les nouvelles lignes : aucun conflit
                    // possible avec les identifiants réels générés par SQL Server.
                    idColumn.AutoIncrementSeed = -1;
                    idColumn.AutoIncrementStep = -1;
                }

                // 2) Données. Fill ouvre et ferme lui-même la connexion.
                adapter.Fill(dataSet, TableName);
            }

            return dataSet;
        }

        /// <summary>
        /// Envoie à la BD toutes les modifications (ajouts, modifications, suppressions)
        /// de la DataTable via SqlDataAdapter.Update et les commandes générées par SqlCommandBuilder.
        /// Le tout est exécuté dans une transaction : tout est enregistré ou rien ne l'est.
        /// </summary>
        public virtual int SaveChanges(DataSet dataSet)
        {
            if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

            DataTable table = dataSet.Tables[TableName]
                ?? throw new ArgumentException($"Le DataSet ne contient pas la table « {TableName} ».");

            if (table.GetChanges() == null) return 0;

            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                using (var adapter = new SqlDataAdapter())
                {
                    // Les lignes ne sont acceptées qu'après le Commit : en cas d'échec,
                    // la DataTable conserve ses modifications et reste synchronisée avec la BD.
                    adapter.AcceptChangesDuringUpdate = false;
                    adapter.SelectCommand = new SqlCommand(SelectAllSql, connection, transaction);

                    using (var builder = new SqlCommandBuilder(adapter))
                    {
                        builder.ConflictOption = ConflictOption.OverwriteChanges;

                        adapter.InsertCommand = builder.GetInsertCommand(true);
                        adapter.UpdateCommand = builder.GetUpdateCommand(true);
                        adapter.DeleteCommand = builder.GetDeleteCommand(true);
                        adapter.InsertCommand.Transaction = transaction;
                        adapter.UpdateCommand.Transaction = transaction;
                        adapter.DeleteCommand.Transaction = transaction;

                        try
                        {
                            int affected = adapter.Update(dataSet, TableName);
                            transaction.Commit();
                            table.AcceptChanges();
                            return affected;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
    }
}
