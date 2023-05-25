namespace ApproACI.Models
{
    public class Objectif
    {
        public int Id { get; set; }
        public double ObjectifGF { get; set; }
        public double ObjectifPF { get; set; }
        public string Mois { get; set; }

        public int ProduitId { get; set; }
        public Produit Produit { get; set; }
    }
}
