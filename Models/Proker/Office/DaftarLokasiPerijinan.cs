using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker.Office
{
    public class DaftarLokasiPerijinan
    {
        public string DaftarLokasiPerijinanID  { get; set; }
        public string NamaLokasi { get; set; }
        public string Alamat { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Pengelola { get; set; }
        public ICollection<Bongkar> Bongkars { get; set; }
        public ICollection<PenLap> PenLaps { get; set; }
    }
   
}
