using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker
{
    public class Jabatan
    {
        public string JabatanID { get; set; }
        public string NamaJabatan { get; set; }

        
        public ICollection<ListPegawai> ListPegawais { get; set; }
    }
}
