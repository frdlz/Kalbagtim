﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models.P2KP.ViewModel
{
    public class MateriP2kpFormViewModel
    {
        public int IDMateriP2kp { get; set; }
        public string MateriName { get; set; }
        public DateTime UploadDate { get; set; }
        public IFormFile File { get; set; }
        public string FileType { get; set; }
        public string JenisFIleID { get; set; }
        public int P2kpID { get; set; }
        public P2kp p2Kp { get; set; }
        
    }
    
}
