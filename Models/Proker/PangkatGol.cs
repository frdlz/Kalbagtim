using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker
{
    public class PangkatGol
    {
        public string PangkatGolID { get; set; }
        public string NamaPangkat { get; set; }

        public ICollection<ListPegawai> ListPegawais { get; set; }
    }
}
