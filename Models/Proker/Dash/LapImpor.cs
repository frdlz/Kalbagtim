using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models.Proker.Dash
{
    public class LapImpor
    {
        public int LapImporID { get; set; }
        [BindProperty, DataType("month")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM}")]
        public DateTime Bulan { get; set; }
       
        public decimal BM { get; set; }
        public decimal JumlahPIB { get; set; }
        public decimal Tonase { get; set; }
        public decimal Devisa { get; set; }

        public string Year { get; set; }
       
    }
}
