using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker.Office
{
    public class PegawaiList
    {
        public string PegawaiListID { get; set; }
        public string Nama { get; set; }
        public string Pangkat { get; set; }
        public string Golongan { get; set; }
        public string Jabatan { get; set; }
        public string Penempatan { get; set; }
        public ICollection<Bongkar> Bongkars { get; set; }
        public ICollection<PenLap> PenLaps { get; set; }
    }
}
