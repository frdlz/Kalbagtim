using System;

namespace ProjectAlpha.Models.Proker.Office
{
    public class LapTimbun
    {
        public string LapTimbunID { get; set; }
        public string NomorLap { get; set; }
        public DateTime Tanggal { get; set; }
        public string BongkarID { get; set; }

        public string Keterangan { get; set; }

        public string NamaPejabat { get; set; }

        public Bongkar Bongkar { get; set; }
    }
}
