using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models
{
    public class Narsum
    {
        public int NarsumID { get; set; }
        [Display(Name = "Narasumber")]
        public string Narasumber { get; set; }
        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        public ICollection<P2kp> P2Kps { get; set; }
    }
}
