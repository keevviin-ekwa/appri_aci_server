using System;

namespace ApproACI.Models
{
    public class Livraison
    {
        public int Id { get; set; }
        public int CommandeId { get; set; }
        
        public DateTime DateLivraison { get; set; }
        public Commande Commande { get; set; }

        public double Quantite { get; set; }
    }
}
