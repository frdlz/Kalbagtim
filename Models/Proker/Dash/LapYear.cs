using System.Collections;
using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker.Dash
{
    public class LapYear
    {
        public int LapYearID { get; set; }
        public string Year { get; set; }

       
       
        public ICollection<TopLokasi> TopLokasis { get; set; }
        public ICollection<TopNegaraAsal> TopNegaraAsals { get; set; }
        
        
        
    }
}
