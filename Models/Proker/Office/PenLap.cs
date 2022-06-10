using System;
using System.Collections;
using System.Collections.Generic;


namespace ProjectAlpha.Models.Proker.Office
{
    public class PenLap
    {
        public string PenlapID { get; set; }
        public string BongkarID { get; set; }
        public string NoPermohonan { get; set; }
        public DateTime TanggalMulai { get; set; }
        public string NoBA { get; set; }
        public DateTime TanggalBA { get; set; }
        public string Keterangan { get; set; }
        public string Petugas1 { get; set; }
        public string NamaPetugas1 { get; set; }
        public string Pangkat1 { get; set; }
        public string Golongan1 { get; set; }
        public string Jabatan1 { get; set; }
        public string Petugas2 { get; set; }
        public string NamaPetugas2 { get; set; }
        public string Pangkat2 { get; set; }
        public string Golongan2 { get; set; }
        public string Jabatan2 { get; set; }
        public StatusKasi Plh { get; set; }
        public string Kasi { get; set; }
        public string NamaKasi { get; set; }
        public string PangkatKasi { get; set; }
        public string GolonganKasi { get; set; }
        public string JabatanKasi { get; set; }
        public string Waktu { get; set; }
        public string Alamat { get; set; }
        public string NamaLokasi { get; set; }
        public string DaftarLokasiPerijinanID { get; set; }
        public string PegawaiListID { get; set; }
        public StatusPenlap Status { get; set; }
        public DaftarLokasiPerijinan DaftarLokasiPerijinan { get; set; }
        public PegawaiList PegawaiList { get; set; }
        
        public Bongkar Bongkar { get; set; }

    }
    public enum StatusKasi
    {
        Kasi,
        Plh,
    }
    public enum StatusPenlap
    {
        layak,
        tidaklayak
    }
}
