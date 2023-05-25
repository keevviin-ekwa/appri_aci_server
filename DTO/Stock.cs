using ApproACI.Models;
using System;

namespace ApproACI.DTO
{
    public class StockDTO
    {
       

        public double StockDebut { get; set; }

        public double StockFin { get; set; }

        public string Mois { get; set; }

        public int Consomation { get; set; }
        public int MatiereId { get; set; }

        public DateTime date { get; set; }



    }
}
