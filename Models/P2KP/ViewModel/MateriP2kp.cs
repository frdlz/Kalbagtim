using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models.P2KP.ViewModel
{
    public class MateriP2kp
    {
        public int MateriP2kpID { get; set; }
        public string MateriName { get; set; }
        public DateTime UploadDate { get; set; }
        public string File { get; set; }
        public string FileType { get; set; }
        public string JenisFIleID { get; set; }
        public int P2kpID { get; set; }
        public P2kp p2Kp { get; set; }
        
        
    }

    
}
