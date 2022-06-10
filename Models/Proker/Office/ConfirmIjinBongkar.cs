using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models.Proker.Office
{
    public class ConfirmIjinBongkar
    {
        public string ConfirmIjinBongkarID { get; set; }
        public string JenisDok { get; set; }
        public string NomorDokumen { get; set; }
        [Display(Name = "Tanggal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TanggalDok { get; set; }
        public string StatusDok { get; set; }
        public string BongkarID { get; set; }
        public Bongkar Bongkar { get; set; }

    }
}
