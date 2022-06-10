using System;

namespace ProjectAlpha.Models.Proker.Office
{
    public class NDBongkar
    {
        public string NDBongkarID { get; set; }
        public string NomorND { get; set; }
        public DateTime TanggalND { get; set; }
        public string BongkarID { get;set; }
        public Bongkar Bongkar { get; set; }
    }
}
