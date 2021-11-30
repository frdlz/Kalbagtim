using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models
{
    public class P2kp
    {
        public int P2kpID { get; set; }

        public string Judul { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Tanggal { get; set; }

        public int NarsumID { get; set; }

        public Narsum Narsum { get; set; }
    }
}
