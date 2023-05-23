using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApproACI.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Reference { get; set; }

        [Required]
        [StringLength(255)]
        public string Designation { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateModification { get; set; }

         public int MarqueId { get; set; }
        public Marque Marque { get; set; }

        public IList<Matiere> Matieres { get; set; }

        public IList<MatiereProduit> MatiereProduits { get; }
    }
}
