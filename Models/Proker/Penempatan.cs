using System;
using ProjectAlpha.Models.Proker.Dash;

namespace ProjectAlpha.Models.Proker
{
    public class Penempatan
    {
        public string PenempatanID { get; set; }
        public DateTime Mulai { get; set; }
        public DateTime Selesai { get; set; }
        public string ListPegawaiID { get; set; }
        public string DaftarLokasiID { get; set; }

        public ListPegawai ListPegawai { get; set; }
        public DaftarLokasi DaftarLokasi { get; set; }
    }
}
