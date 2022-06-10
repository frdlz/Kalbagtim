using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker.Dash
{
    public class DaftarLokasi
    {
        public string DaftarLokasiID { get; set; }
        public string NamaLokasi { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Pengelola { get; set; }
        public string Jenis { get; set; }
        public string Keterangan { get; set; }
        
        public ICollection<Penempatan> Penempatans { get; set; }

    }
}
