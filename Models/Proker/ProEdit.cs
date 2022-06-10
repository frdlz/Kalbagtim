using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAlpha.Models
{
    public class ProEdit
    {
        public Program Program { get; set; }
        public IEnumerable<AppUser> Anggota { get; set; }
        public IEnumerable<AppUser> NonAnggota { get; set; }
    }
}
