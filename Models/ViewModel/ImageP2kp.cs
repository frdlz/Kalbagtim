using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models.ViewModel
{
    public class ImageP2kp
    {
        public int ImageP2kpID { get; set; }
        public string ImageName { get; set; }
        public DateTime UploadDate { get; set; }
        public string Image { get; set; }
        public int P2kpID { get; set; }
        public P2kp p2Kp { get; set; }
    }
}
