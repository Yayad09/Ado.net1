using System;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace TP1_LSDA.Data
{
    /// <summary>
    /// Initialisation SQL FIRST : si la base TP1_LSDA (ou ses tables) n'existe pas, exécute
    /// via ADO.NET les scripts du dossier DatabaseScripts, dans l'ordre :
    /// create_database.sql → create_tables.sql → insert_data.sql.
    /// La chaîne de connexion d'app.config n'est jamais modifiée : on dérive seulement
    /// une connexion vers [master] pour pouvoir créer la base.
    /// </summary>
    public class DatabaseInitializer
    {
        private static readonly Regex GoSeparator =
            new Regex(@"^\s*GO\s*(?:--.*)?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        private readonly string _connectionString;
        private readonly string _masterConnectionString;
        private readonly string _databaseName;
        private readonly string _scriptsFolder;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;

            var builder = new SqlConnectionStringBuilder(connectionString);
            _databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            _masterConnectionString = builder.ConnectionString;

            _scriptsFolder = Path.Combine(AppContext.BaseDirectory, "DatabaseScripts");
        }

        /// <summary>Retourne vrai si les scripts ont été exécutés (base créée ou recréée).</summary>
        public bool EnsureDatabase()
        {
            if (DatabaseExists() && TablesExist()) return false;

            ExecuteScriptFile(_masterConnectionString, "create_database.sql");
            ExecuteScriptFile(_masterConnectionString, "create_tables.sql");
            ExecuteScriptFile(_masterConnectionString, "insert_data.sql");
            SqlConnection.ClearAllPools();
            return true;
        }

        private bool DatabaseExists()
        {
            using (var connection = new SqlConnection(_masterConnectionString))
            using (var command = new SqlCommand("SELECT DB_ID(@Nom);", connection))
            {
                command.Parameters.AddWithValue("@Nom", _databaseName);
                connection.Open();
                return command.ExecuteScalar() != DBNull.Value;
            }
        }

        private bool TablesExist()
        {
            const string sql =
                "SELECT CASE WHEN OBJECT_ID(N'dbo.Lieu', N'U') IS NOT NULL " +
                "AND OBJECT_ID(N'dbo.Personnage', N'U') IS NOT NULL " +
                "AND OBJECT_ID(N'dbo.Mission', N'U') IS NOT NULL THEN 1 ELSE 0 END;";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) == 1;
            }
        }

        private void ExecuteScriptFile(string connectionString, string fileName)
        {
            string path = Path.Combine(_scriptsFolder, fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException($"Script SQL introuvable : {path}");

            string script = File.ReadAllText(path);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Un lot (batch) par séparateur GO, comme dans SSMS.
                foreach (string batch in GoSeparator.Split(script))
                {
                    if (string.IsNullOrWhiteSpace(batch)) continue;

                    using (var command = new SqlCommand(batch, connection))
                    {
                        command.CommandTimeout = 120;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
