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

        [Display(Name = "Materi")]
        public string Judul { get; set; }

        [Display(Name = "Tanggal")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tanggal { get; set; }

        [Display(Name = "Mulai")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd:MM:yyyy}")]
        public DateTime JamMulai { get; set; }

        [Display(Name = "Selesai")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime JamSelesai { get; set; }

        [Display(Name = "Tempat")]
        public string Tempat { get; set; }

        public StatusP2kp Status { get; set; }
        public DateTime WaktuBuat { get; set; }
        public DateTime WaktuSelesai { get; set; }



        [Display(Name = "Narasumber")]
        public int NarsumID { get; set; }

        public Narsum Narsum { get; set; }
    }
    public enum StatusP2kp
    {
        belum,
        selesai
    }
}
