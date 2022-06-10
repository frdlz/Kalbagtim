using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAlpha.Models.Proker.ProkerViewModel
{
   
        public class IndexViewModel
        {
            public IEnumerable<ProgramKerja> ProgramKerjas { get; set; }
            public IEnumerable<Update> Updates { get; set; }
           
            
            public int TotalProgram { get; set; }
            public int TotalProgress { get; set; }
            public int TotalPegawai { get; set; }
            public int TotalRapat { get; set; }
            
            public DateTime TahunIni { get; set; }
        }
    
}
