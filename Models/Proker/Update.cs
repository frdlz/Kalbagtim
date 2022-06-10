using System;

namespace ProjectAlpha.Models.Proker
{
    public class Update
    {
        public string UpdateID { get; set; }
        public string ProgramID { get; set; }
        public string Keterangan { get; set; }
        public DateTime Tanggal { get; set; }

        public StatusUpdate Status { get; set; }

        public ProgramKerja Program { get; set; }
    }

    public enum StatusUpdate
    {
        create,
        update,
        finish
    }
}
