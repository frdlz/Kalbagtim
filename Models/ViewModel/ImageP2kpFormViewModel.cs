using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models.ViewModel
{
    public class ImageP2kpFormViewModel
    {
        public int IDImageP2kp { get; set; }
        public string ImageName { get; set; }
        public DateTime UploadDate { get; set; }
        public IFormFile Image { get; set; }
        public int P2kpID { get; set; }
        public P2kp p2Kp { get; set; }
    }
}
