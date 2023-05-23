using ApproACI.Models;

namespace ApproACI.DTO
{
    public class MatiereProduitDTO
    {
        public int ProduitId { get; set; }

        public int MatiereId { get; set; } 

        public double ContributionMatiereGF { get; set; }

        public double ContributionMatierePF { get; set; }
    }

    public class ResultMatiereProduitDTO
    {
        public ProductDTO Produit { get; set; }

        public MatiereDTO Matiere { get; set; }

        public double ContributionMatierePF { get; set; }
        public double ContributionMatiereGF { get; set; }
    }
}
