namespace ApproACI.Models
{
    public class Objectif
    {
        public int Id { get; set; }
        public double ObjectifGF { get; set; }

        public double ObjectifPF { get; set; }
        public double ObjectifAtteindGF { get; set; }
        public double ObjectifAtteindPF { get; set; }

        public string Mois { get; set; }
    }
}
