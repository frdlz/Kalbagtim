using System.ComponentModel.DataAnnotations;

namespace ProjectAlpha.Models.Proker.Dash
{
    public class Dashboard
    {
        [Key]
        public string DashboardID { get; set; }
        public string DashboardName { get; set; }
    }
}
