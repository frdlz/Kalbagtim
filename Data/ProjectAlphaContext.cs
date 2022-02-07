using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectAlpha.Models;
using ProjectAlpha.Models.ViewModel;
using WBKNET.Models.Frontdesk;
using ProjectAlpha.Models.Side;

namespace ProjectAlpha.Data
{
    public class ProjectAlphaContext : IdentityDbContext<AppUser>
    {
        public ProjectAlphaContext (DbContextOptions<ProjectAlphaContext> options)
            : base(options)     
        {
        }

        public DbSet<ProjectAlpha.Models.P2kp> P2kp { get; set; }

        public DbSet<ProjectAlpha.Models.Narsum> Narsum { get; set; }

        public DbSet<ProjectAlpha.Models.ViewModel.JenisFile> JenisFile { get; set; }

        public DbSet<WBKNET.Models.Frontdesk.Appointment> Appointment { get; set; }

        public DbSet<WBKNET.Models.Frontdesk.LayananFrontdesk> LayananFrontdesk { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var keysProperties = builder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }
        }

        public DbSet<ProjectAlpha.Models.Side.Vaksin> Vaksin { get; set; }
    }
}
