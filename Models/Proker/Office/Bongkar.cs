using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models.Proker.Office
{
    public class Bongkar
    {
        public string BongkarID { get; set; }
        public string NoPermohonan { get; set; }
        [Display(Name = "Tanggal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TanggalPermohonan { get; set; }
        public string Hal { get; set; }
        public string NomorBC11 { get; set; }
        [Display(Name = "Tanggal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TanggalBC11 { get; set; }
        public string NamaSarkut { get; set; }
        public string NoBL { get; set; }
        [Display(Name = "Tanggal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TanggalBL { get; set; }
        public decimal Bruto { get; set; }
        public decimal Volume { get; set; }
        public string DaftarLokasiPerijinanID { get; set; }
        public string PegawaiListID { get; set; }
        
        public string Alasan { get; set; }
        public string PenggunaJasaID { get; set; }
        public string CatatanDisposisi { get; set; }
        public string KasiPabeanID { get; set; }
        public DateTime WkCreate { get; set; }
        public DateTime WkSubmit { get; set; }
        public DateTime WkSelesai { get; set; }
        public DateTime WkTolak { get; set; }

        public string PetugasTerimaPabean { get; set; }
        public string PetugasTolakPabean { get; set; }
        public DateTime WkPetugasTerimaPabean { get; set; }

        public string PetugasTerimaP2 { get; set; }
        public DateTime WkPetugasTerimaP2 { get; set; }

        public StatusKasiPabean StatusKasiPabean { get; set; }
        public string KasiPabean { get; set; }
        public string NamaKasiPabean { get; set; }
        public string JabatanKasiPabean { get; set; }
        public DateTime WkKasiPabean { get; set; }
        public StatusKasiP2 StatusKasiP2 { get; set; }
        public string KasiP2 { get; set; }
        public DateTime WkKasiP2 { get; set; }
        public StatusKakap StatusKakap { get; set; }
        public string Kakap { get; set; }
        public string NamaKakap { get; set; }
        public string JabatanKakap { get; set; }
        public DateTime WkKakap { get; set; }

        public StatusBongkar Status { get; set; }
        public StatusPabean Pabean { get; set; }
        public StatusP2 P2 { get; set; }
        public StatusKK KK { get; set; }
        public DaftarLokasiPerijinan DaftarLokasiPerijinan { get; set; }
        public PegawaiList PegawaiList { get; set; }
        public PenggunaJasa PenggunaJasa { get; set; }
        public ICollection<Disposisi> Disposisis { get; set; }
        public ICollection<PosBarang> PosBarangs { get; set; }
        public ICollection<ConfirmIjinBongkar> ConfirmIjinBongkars { get; set; }
        public ICollection<PenLap> PenLaps { get; set; }
        public ICollection<LapTimbun> LapBongkars { get; set; }
        public ICollection<NDBongkar> NDBongkars { get; set; }

    }
    public enum StatusBongkar
    {
        create,
        draft,
        submit,
        terima,
        tolak,
        selesai

    }
    public enum StatusPabean 
    {
        terima,
        proses,
        tolak,
        kirimp2,
        selesai,
        kirimkk,
        selesaip2,
        kasisetuju,
        kasitolak
        
    }
    public enum StatusP2
    {
        terima,
        proses,
        tolak,
        kirimpabean,
        selesai
    }
    public enum StatusKK
    {
        terima,
        proses,
        TTD,
        tolak,
        selesai
    }

    public enum StatusKasiPabean
    {
        Kasi,
        Plh,
    }
    public enum StatusKasiP2
    {
        Kasi,
        Plh,
    }
    public enum StatusKakap
    {
        Kakap,
        Plh,
        Plt,
    }
    
}
