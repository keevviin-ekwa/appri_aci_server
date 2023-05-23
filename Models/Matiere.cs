using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApproACI.Models
{
    public class Matiere
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Reference { get; set; }


        [Required]
        [StringLength(255)]
        public string Designation { get; set; }

        [Required]
        [StringLength(50)]
        public string Origine { get; set; }

        [Required]
        public double PrixUnitaire { get; set; }

        [Required]
        public double Colissage { get; set; }

        [Required]
        public int DelaisAppro { get; set; }

        
        public DateTime DateCreation { get; set; }

        public DateTime DateModification { get; set; }
        public List<Stock> Stocks { get; set; } = new List<Stock>();
        public List<Produit> Produits { get; set; }
        public List<MatiereProduit> MatiereProduits { get;}
    }
}
