using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models.Proker
{
    public class ProgramKerja
    {
        [Key]
        public string ProgramID { get; set; }
        public string NamaProgram { get; set; }
        public string Keterangan { get; set; }
        public StatusProgram Status { get; set; }
        public ICollection<Update> Update { get; set; }

    }

    public enum StatusProgram
    {
        create,
        update,
        finish,
    }
}
