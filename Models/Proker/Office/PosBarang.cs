namespace ProjectAlpha.Models.Proker.Office
{
    public class PosBarang
    {
        public string PosBarangID { get; set; }
        public string NoPos { get; set; }
        public string BL { get; set; }
        public string UraianBarang { get; set; }
        public decimal Jumlah { get; set; }
        public string JenisKemasan { get; set; }
        public string Ukuran { get; set; }
        public string Satuan { get; set; }
        public string Kedapatan { get; set; }
        public string Selisih { get; set; }
        public string Keterangan { get; set; }
        public string Owner { get; set; }
        public string Pemeriksa { get; set; }
        public string BongkarID { get; set; }
        public Bongkar Bongkar { get; set; }
    }
}
