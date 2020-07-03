using System;
using System.Collections.Generic;

namespace API
{
    public partial class Ofise
    {
        public int IdOfis { get; set; }
        public string NameOfis { get; set; }
        public string Descriptio { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClouse { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Logo { get; set; }
    }
}
