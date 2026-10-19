namespace TP1_LSDA.Domain
{
    /// <summary>Lieu de la Terre du Milieu (table dbo.Lieu).</summary>
    public class Lieu
    {
        public int Id { get; set; }
        public string NomLieu { get; set; }
        public string Royaume { get; set; }
        public int Dangerosite { get; set; }

        public override string ToString() => $"{NomLieu} ({Royaume})";
    }
}
