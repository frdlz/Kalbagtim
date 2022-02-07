using System;
using System.Collections.Generic;

namespace ProjectAlpha.Models.Side
{
    public class VaksinViewModel
    {
        public IEnumerable<Vaksin> vaksins { get; set; }
       
        public decimal TotalPegawaai { get; set; }
        public decimal TotalPegawaai2 { get; set; }
        public decimal TotalVaksin1 { get; set; }
        public decimal TotalVaksin1Non { get; set; }
        public decimal TotalVaksin2 { get; set; }
        public decimal TotalVaksin2Non { get; set; }
        public decimal TotalVaksin3 { get; set; }
        public decimal TotalVaksin3Non { get; set; }
        
    }
}
