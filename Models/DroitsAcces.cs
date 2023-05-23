using System;

namespace ApproACI.Models
{
    public class DroitsAcces
    {
        public int Id { get; set; }
        public string DesignationTechnique { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateDerniereMaj { get; set; }
    }
}
