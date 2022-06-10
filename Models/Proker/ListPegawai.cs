using System.Collections.Generic;
using ProjectAlpha.Models.Proker.Dash;

namespace ProjectAlpha.Models.Proker
{
    public class ListPegawai
    {
        public string ListPegawaiID { get; set; }
        public string NamaPegawai { get; set; }
        public string JabatanID { get; set; }
        public string PangkatGolID { get; set; }
        public ICollection<Penempatan> Penempatans { get; set; }
        public Jabatan Jabatan { get; set; }
        public PangkatGol PangkatGol { get; set; }
        
    }
}
