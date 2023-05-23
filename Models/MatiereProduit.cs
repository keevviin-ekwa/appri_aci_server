using Microsoft.EntityFrameworkCore;

namespace ApproACI.Models
{


    public class MatiereProduit
    {
        public int ProduitId { get; set; }

        public int MatiereId { get; set; } 

        public double ContributionMatiereGF { get; set; }
         
        public double contributionMatierePF { get; set; }   
        public Produit Produit { get; set; }    
         
        public Matiere Matiere { get; set; }  
    }
}
