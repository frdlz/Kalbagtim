using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker.Office
{
    public class PenggunaJasa
    {
        public string PenggunaJasaID { get; set; }
        public string NamaPerusahaan { get; set; }
        public string NPWP { get; set; }
        public string Alamat { get; set; }
        public string Kota { get; set; }

        public ICollection<Bongkar> Bongkars { get; set; }
    }
}
