using ApproACI.Models;
using System;

namespace ApproACI.DTO
{
    public class StockDTO
    {
        public int Id { get; set; }

        public double StockDebut { get; set; }

        public double StockFin { get; set; }

        public string Mois { get; set; }


        public int MatiereId { get; set; }

    }
}
