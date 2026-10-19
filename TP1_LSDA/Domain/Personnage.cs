namespace TP1_LSDA.Domain
{
    /// <summary>Personnage pouvant être affecté à une mission (table dbo.Personnage).</summary>
    public class Personnage
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Race { get; set; }
        public string Faction { get; set; }

        public override string ToString() => $"{Nom} ({Race})";
    }
}
