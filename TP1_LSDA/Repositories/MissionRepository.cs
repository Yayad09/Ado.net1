using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using TP1_LSDA.Domain;

namespace TP1_LSDA.Repositories
{
    public class MissionRepository : RepositoryBase<Mission>
    {
        /// <summary>SELECT avec jointures : permet d'afficher le nom du lieu et du personnage.</summary>
        private const string SelectDetailSql =
            "SELECT m.Id, m.LieuId, l.NomLieu, m.PersonnageId, p.Nom AS NomPersonnage, m.DateDebut, m.DateFin " +
            "FROM dbo.Mission AS m " +
            "INNER JOIN dbo.Lieu AS l ON l.Id = m.LieuId " +
            "INNER JOIN dbo.Personnage AS p ON p.Id = m.PersonnageId";

        public MissionRepository(string connectionString) : base(connectionString) { }

        public override string TableName => "Mission";

        protected override string SelectAllSql =>
            "SELECT Id, LieuId, PersonnageId, DateDebut, DateFin FROM dbo.Mission";

        protected override Mission Map(SqlDataReader reader)
        {
            var mission = new Mission
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                LieuId = reader.GetInt32(reader.GetOrdinal("LieuId")),
                PersonnageId = reader.GetInt32(reader.GetOrdinal("PersonnageId")),
                DateDebut = reader.GetDateTime(reader.GetOrdinal("DateDebut")),
                DateFin = reader.GetDateTime(reader.GetOrdinal("DateFin"))
            };

            if (HasColumn(reader, "NomLieu")) mission.NomLieu = reader.GetString(reader.GetOrdinal("NomLieu"));
            if (HasColumn(reader, "NomPersonnage")) mission.NomPersonnage = reader.GetString(reader.GetOrdinal("NomPersonnage"));

            return mission;
        }

        private static bool HasColumn(SqlDataReader reader, string name)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), name, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        public override List<Mission> GetAll()
        {
            return ExecuteReader(SelectDetailSql + " ORDER BY m.DateDebut;");
        }

        public override Mission GetById(int id)
        {
            List<Mission> list = ExecuteReader(SelectDetailSql + " WHERE m.Id = @Id;", Param("@Id", SqlDbType.Int, id));
            return list.Count > 0 ? list[0] : null;
        }

        public override int Add(Mission entity)
        {
            const string sql =
                "INSERT INTO dbo.Mission (LieuId, PersonnageId, DateDebut, DateFin) " +
                "VALUES (@LieuId, @PersonnageId, @DateDebut, @DateFin); " +
                // SCOPE_IDENTITY plutôt que OUTPUT : OUTPUT sans INTO est interdit sur une table avec trigger.
                "SELECT CAST(SCOPE_IDENTITY() AS int);";

            return Convert.ToInt32(ExecuteScalar(sql,
                Param("@LieuId", SqlDbType.Int, entity.LieuId),
                Param("@PersonnageId", SqlDbType.Int, entity.PersonnageId),
                Param("@DateDebut", SqlDbType.DateTime, entity.DateDebut),
                Param("@DateFin", SqlDbType.DateTime, entity.DateFin)));
        }

        public override bool Update(Mission entity)
        {
            const string sql =
                "UPDATE dbo.Mission SET LieuId = @LieuId, PersonnageId = @PersonnageId, " +
                "DateDebut = @DateDebut, DateFin = @DateFin WHERE Id = @Id;";

            return ExecuteNonQuery(sql,
                Param("@Id", SqlDbType.Int, entity.Id),
                Param("@LieuId", SqlDbType.Int, entity.LieuId),
                Param("@PersonnageId", SqlDbType.Int, entity.PersonnageId),
                Param("@DateDebut", SqlDbType.DateTime, entity.DateDebut),
                Param("@DateFin", SqlDbType.DateTime, entity.DateFin)) > 0;
        }

        // ----- Vérification de conflit (mode connecté : requête paramétrée + SqlDataReader) -----

        /// <summary>
        /// Retourne les missions du même lieu dont la plage chevauche [debut, fin[.
        /// Deux plages se chevauchent si : existante.DateDebut &lt; nouvelle.DateFin
        /// ET existante.DateFin &gt; nouvelle.DateDebut.
        /// </summary>
        public List<Mission> TrouverConflits(int lieuId, DateTime debut, DateTime fin, int? missionIdExclue = null)
        {
            const string sql =
                SelectDetailSql +
                " WHERE m.LieuId = @LieuId AND m.DateDebut < @Fin AND m.DateFin > @Debut" +
                " AND (@IdExclu IS NULL OR m.Id <> @IdExclu)" +
                " ORDER BY m.DateDebut;";

            return ExecuteReader(sql,
                Param("@LieuId", SqlDbType.Int, lieuId),
                Param("@Debut", SqlDbType.DateTime, debut),
                Param("@Fin", SqlDbType.DateTime, fin),
                Param("@IdExclu", SqlDbType.Int, missionIdExclue));
        }

        public bool ExisteConflit(int lieuId, DateTime debut, DateTime fin, int? missionIdExclue = null)
        {
            return TrouverConflits(lieuId, debut, fin, missionIdExclue).Count > 0;
        }

        // ----- Recherche filtrée (mode connecté) -----

        /// <summary>Missions qui se déroulent (au moins en partie) dans la période [debut, fin].</summary>
        public List<Mission> RechercherParPeriode(DateTime debut, DateTime fin)
        {
            const string sql =
                SelectDetailSql +
                " WHERE m.DateDebut <= @Fin AND m.DateFin >= @Debut ORDER BY m.DateDebut;";

            return ExecuteReader(sql,
                Param("@Debut", SqlDbType.DateTime, debut),
                Param("@Fin", SqlDbType.DateTime, fin));
        }

        // ----- Liste globale (mode déconnecté) -----

        /// <summary>DataSet contenant toutes les missions avec le nom du lieu et du personnage (jointure).</summary>
        public DataSet GetMissionsDetailDataSet()
        {
            var dataSet = new DataSet("TP1_LSDA");

            using (SqlConnection connection = CreateConnection())
            using (var adapter = new SqlDataAdapter(SelectDetailSql + " ORDER BY m.DateDebut;", connection))
            {
                adapter.Fill(dataSet, "MissionsDetail");
            }

            return dataSet;
        }
    }
}
