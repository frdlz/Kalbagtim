using System;

namespace ProjectAlpha.Models.Proker.Dash
{
    public class TopLokasi
    {
        public int TopLokasiID { get; set; }
        public DateTime Bulan { get; set; }
        public string Nama { get; set; }
        public decimal JumlahPIB { get; set; }
        public decimal BM { get; set; }
        public int LapYearID { get; set; }
        public LapYear LapYear { get; set; }
    }
}
