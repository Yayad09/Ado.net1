using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TP1_LSDA.Domain;

namespace TP1_LSDA.Repositories
{
    public class PersonnageRepository : RepositoryBase<Personnage>
    {
        public PersonnageRepository(string connectionString) : base(connectionString) { }

        public override string TableName => "Personnage";

        protected override string SelectAllSql =>
            "SELECT Id, Nom, Race, Faction FROM dbo.Personnage";

        protected override Personnage Map(SqlDataReader reader)
        {
            return new Personnage
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Nom = reader.GetString(reader.GetOrdinal("Nom")),
                Race = reader.GetString(reader.GetOrdinal("Race")),
                Faction = reader.GetString(reader.GetOrdinal("Faction"))
            };
        }

        public override List<Personnage> GetAll()
        {
            return ExecuteReader(SelectAllSql + " ORDER BY Nom;");
        }

        public override int Add(Personnage entity)
        {
            const string sql =
                "INSERT INTO dbo.Personnage (Nom, Race, Faction) " +
                "OUTPUT INSERTED.Id VALUES (@Nom, @Race, @Faction);";

            return Convert.ToInt32(ExecuteScalar(sql,
                Param("@Nom", SqlDbType.NVarChar, 100, entity.Nom),
                Param("@Race", SqlDbType.NVarChar, 50, entity.Race),
                Param("@Faction", SqlDbType.NVarChar, 50, entity.Faction)));
        }

        public override bool Update(Personnage entity)
        {
            const string sql =
                "UPDATE dbo.Personnage SET Nom = @Nom, Race = @Race, Faction = @Faction WHERE Id = @Id;";

            return ExecuteNonQuery(sql,
                Param("@Id", SqlDbType.Int, entity.Id),
                Param("@Nom", SqlDbType.NVarChar, 100, entity.Nom),
                Param("@Race", SqlDbType.NVarChar, 50, entity.Race),
                Param("@Faction", SqlDbType.NVarChar, 50, entity.Faction)) > 0;
        }

        // ----- Recherches (mode connecté) -----

        /// <summary>Personnages dont la race contient le texte saisi (requête paramétrée).</summary>
        public List<Personnage> RechercherParRace(string race)
        {
            const string sql =
                "SELECT Id, Nom, Race, Faction FROM dbo.Personnage " +
                "WHERE Race LIKE @Race ORDER BY Race, Nom;";

            return ExecuteReader(sql, Param("@Race", SqlDbType.NVarChar, 52, "%" + (race ?? "").Trim() + "%"));
        }

        /// <summary>Liste distincte des races (pour alimenter le filtre de recherche).</summary>
        public List<string> GetRaces()
        {
            return ExecuteReader("SELECT DISTINCT Race FROM dbo.Personnage ORDER BY Race;",
                r => r.GetString(0));
        }
    }
}
