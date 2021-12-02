using ProjectAlpha.Models.P2KP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models.ViewModel
{
    public class JenisFile
    {
        public int JenisFileID { get; set; }
        public string FileType { get; set; }
        public ICollection<MateriP2kp> MateriP2Kp { get; set; }
    }
}
