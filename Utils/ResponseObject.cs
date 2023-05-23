using ApproACI.Models;
using System;
using System.Collections.Generic;

namespace ApproACI.Utils
{
    public class ResponseObject<T>
    {
        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string DevelopperMessage { get; set; }

        
    }
}
