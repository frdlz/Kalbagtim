using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProjectAlpha.Models.Proker.Dash.ViewModel
{
    public class SelectViewModelImpor
    {
        public LapImporViewModel LapImporViewModel { get; set; }
        public List<LapImpor> LapImpor { get; set; }
        public SelectList Year { get; set; }
        public string SelectYear { get; set; }
    }
}
