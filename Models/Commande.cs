using System;

namespace ApproACI.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public int SemaineCommande { get; set; }

        public DateTime DateLivraison { get; set; }

        public double Quantite { get; set; }

        public DateTime DateCommande { get; set; }

        public int MatiereId { get; set; }

        public Matiere Matiere { get; set; }    
    }
}
