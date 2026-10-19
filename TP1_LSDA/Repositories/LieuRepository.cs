using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TP1_LSDA.Domain;

namespace TP1_LSDA.Repositories
{
    public class LieuRepository : RepositoryBase<Lieu>
    {
        public LieuRepository(string connectionString) : base(connectionString) { }

        public override string TableName => "Lieu";

        protected override string SelectAllSql =>
            "SELECT Id, NomLieu, Royaume, Dangerosite FROM dbo.Lieu";

        protected override Lieu Map(SqlDataReader reader)
        {
            return new Lieu
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                NomLieu = reader.GetString(reader.GetOrdinal("NomLieu")),
                Royaume = reader.GetString(reader.GetOrdinal("Royaume")),
                Dangerosite = reader.GetInt32(reader.GetOrdinal("Dangerosite"))
            };
        }

        public override List<Lieu> GetAll()
        {
            return ExecuteReader(SelectAllSql + " ORDER BY NomLieu;");
        }

        public override int Add(Lieu entity)
        {
            const string sql =
                "INSERT INTO dbo.Lieu (NomLieu, Royaume, Dangerosite) " +
                "VALUES (@NomLieu, @Royaume, @Dangerosite); SELECT CAST(SCOPE_IDENTITY() AS int);";

            return Convert.ToInt32(ExecuteScalar(sql,
                Param("@NomLieu", SqlDbType.NVarChar, 100, entity.NomLieu),
                Param("@Royaume", SqlDbType.NVarChar, 50, entity.Royaume),
                Param("@Dangerosite", SqlDbType.Int, entity.Dangerosite)));
        }

        public override bool Update(Lieu entity)
        {
            const string sql =
                "UPDATE dbo.Lieu SET NomLieu = @NomLieu, Royaume = @Royaume, Dangerosite = @Dangerosite " +
                "WHERE Id = @Id;";

            return ExecuteNonQuery(sql,
                Param("@Id", SqlDbType.Int, entity.Id),
                Param("@NomLieu", SqlDbType.NVarChar, 100, entity.NomLieu),
                Param("@Royaume", SqlDbType.NVarChar, 50, entity.Royaume),
                Param("@Dangerosite", SqlDbType.Int, entity.Dangerosite)) > 0;
        }

        // ----- Recherches (mode connecté) -----

        /// <summary>Lieux dont le royaume contient le texte saisi (requête paramétrée, pas de concaténation).</summary>
        public List<Lieu> RechercherParRoyaume(string royaume)
        {
            const string sql =
                "SELECT Id, NomLieu, Royaume, Dangerosite FROM dbo.Lieu " +
                "WHERE Royaume LIKE @Royaume ORDER BY Royaume, NomLieu;";

            return ExecuteReader(sql, Param("@Royaume", SqlDbType.NVarChar, 52, "%" + (royaume ?? "").Trim() + "%"));
        }

        /// <summary>Lieux n'ayant aucune mission qui chevauche la plage [debut, fin[.</summary>
        public List<Lieu> RechercherDisponibles(DateTime debut, DateTime fin)
        {
            const string sql =
                "SELECT l.Id, l.NomLieu, l.Royaume, l.Dangerosite FROM dbo.Lieu AS l " +
                "WHERE NOT EXISTS (SELECT 1 FROM dbo.Mission AS m " +
                "                  WHERE m.LieuId = l.Id AND m.DateDebut < @Fin AND m.DateFin > @Debut) " +
                "ORDER BY l.NomLieu;";

            return ExecuteReader(sql,
                Param("@Debut", SqlDbType.DateTime, debut),
                Param("@Fin", SqlDbType.DateTime, fin));
        }

        /// <summary>Liste distincte des royaumes (pour alimenter le filtre de recherche).</summary>
        public List<string> GetRoyaumes()
        {
            return ExecuteReader("SELECT DISTINCT Royaume FROM dbo.Lieu ORDER BY Royaume;",
                r => r.GetString(0));
        }
    }
}
