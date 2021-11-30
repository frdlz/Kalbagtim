using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models
{
    public class Narsum
    {
        public int NarsumID { get; set; }
        public string Narasumber { get; set; }
        public string Keterangan { get; set; }

        public ICollection<P2kp> P2Kps { get; set; }
    }
}
