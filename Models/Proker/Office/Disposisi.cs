namespace ProjectAlpha.Models.Proker.Office
{
    public class Disposisi
    {
        public int DisposisiID { get; set; }
        public string DisposisiName { get; set; }
        public string BongkarID { get; set; }
        public Bongkar Bongkar { get; set; }
    }
}
