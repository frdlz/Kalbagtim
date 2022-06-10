using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;


namespace ProjectAlpha.Models.Proker.Dash.ViewModel
{
    public class LapImporViewModel
    {
        public List<LapImpor> lapImpors { get; set; }
        public List<TopImporBM> TopImporBMs { get; set; }
        public List<TopLokasi> TopLokasis { get; set; }
        public List<TopNegaraAsal> TopNegaraAsal { get; set; }
        public List<TopKomoditi> TopKomoditis { get; set; }
        public List<TopImportirDev> TopImportirDevs { get; set; }
        public List<DaftarLokasi> DaftarLokasis { get; set; }
        public TopNegaraAsal TopNegaraAsals { get; set; }
        public List<Target> Targets { get; set; }
        public decimal TotalBM { get; set; }
        public decimal TargetBMYear { get; set; }
        public decimal PercentBMYear { get; set; }
        public decimal TotalPIB { get; set; }
        public decimal TotalTon { get; set; }
        public decimal TotalDevisa { get; set; }
        public SelectList Year { get; set; }
        public string YearSelect { get; set; }
        
    }
}
