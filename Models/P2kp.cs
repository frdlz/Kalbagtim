using ProjectAlpha.Models.P2KP.ViewModel;
using ProjectAlpha.Models.ViewModel;
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
        [Required]
        public DateTime Tanggal { get; set; }

        [Display(Name = "Mulai")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd:MM:yyyy}")]
        [Required]
        public DateTime JamMulai { get; set; }

        [Display(Name = "Selesai")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        [Required]
        
        public DateTime JamSelesai { get; set; }

        [Display(Name = "Tempat")]
      
        public string Tempat { get; set; }

        public StatusP2kp Status { get; set; }
        public DateTime WaktuBuat { get; set; }
        public DateTime WaktuSelesai { get; set; }



        [Display(Name = "Narasumber")]
        [Required]
        public int NarsumID { get; set; }

        public Narsum Narsum { get; set; }
        public ICollection<ImageP2kp> ImageP2kp { get; set; }
        public ICollection<MateriP2kp> MateriP2Kp { get; set; }
    }
    public enum StatusP2kp
    {
        belum,
        selesai
    }
}
