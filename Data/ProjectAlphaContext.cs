using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Models;
using ProjectAlpha.Models.ViewModel;

namespace ProjectAlpha.Data
{
    public class ProjectAlphaContext : DbContext
    {
        public ProjectAlphaContext (DbContextOptions<ProjectAlphaContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectAlpha.Models.P2kp> P2kp { get; set; }

        public DbSet<ProjectAlpha.Models.Narsum> Narsum { get; set; }

        public DbSet<ProjectAlpha.Models.ViewModel.JenisFile> JenisFile { get; set; }
    }
}
