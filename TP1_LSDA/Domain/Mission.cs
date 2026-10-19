using System;

namespace TP1_LSDA.Domain
{
    /// <summary>Mission planifiée dans un lieu, pour un personnage, sur une plage de temps (table dbo.Mission).</summary>
    public class Mission
    {
        public int Id { get; set; }
        public int LieuId { get; set; }
        public int PersonnageId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        // Colonnes d'affichage obtenues par jointure (non persistées dans dbo.Mission)
        public string NomLieu { get; set; }
        public string NomPersonnage { get; set; }

        /// <summary>Vrai si les deux plages se chevauchent (les bornes qui se touchent ne sont pas un conflit).</summary>
        public bool Chevauche(DateTime debut, DateTime fin) => DateDebut < fin && DateFin > debut;
    }
}
