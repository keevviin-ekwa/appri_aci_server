namespace ApproACI.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public string SemaineCommande { get; set; }

        public string DateLivraison { get; set; }

        public string Quantite { get; set; }

        public int MatiereId { get; set; }

        public Matiere Matiere { get; set; }    
    }
}
