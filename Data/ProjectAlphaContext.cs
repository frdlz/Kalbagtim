using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectAlpha.Models;
using ProjectAlpha.Models.ViewModel;
using ProjectAlpha.Models.Frontdesk;
using ProjectAlpha.Models.Side;
using ProjectAlpha.Models.Proker;
using ProjectAlpha.Models.Proker.Dash;
using ProjectAlpha.Models.Proker.Office;

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

        public DbSet<ProjectAlpha.Models.Frontdesk.Appointment> Appointment { get; set; }

        public DbSet<ProjectAlpha.Models.Frontdesk.LayananFrontdesk> LayananFrontdesk { get; set; }

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
        public DbSet<ListPegawai> ListPegawai { get; set; }
        public DbSet<LapImpor> LapImpor { get;set; }
        public DbSet<TopImporBM> TopImporBM { get;  set; }
        public DbSet<TopImportirDev> TopImportirDev { get;  set; }
        public DbSet<TopKomoditi> TopKomoditi { get;  set; }
        public DbSet<TopNegaraAsal> TopNegaraAsal { get;  set; }
        public DbSet<DaftarLokasi> DaftarLokasi { get;  set; }
        public DbSet<Dashboard> Dashboard { get;  set; }
        public DbSet<Target> Target { get;  set; }
        public DbSet<TopLokasi> TopLokasi { get;  set; }
        public DbSet<LapYear> LapYear { get;  set; }
        public DbSet<Jabatan> Jabatan { get;  set; }
        public DbSet<PangkatGol> PangkatGol { get;  set; }
        public DbSet<Bongkar> Bongkar { get;  set; }
        public DbSet<PosBarang> PosBarang { get;  set; }
        public DbSet<DaftarLokasiPerijinan> DaftarLokasiPerijinan { get;  set; }
        public DbSet<PenLap> PenLap { get;  set; }
        public DbSet<ConfirmIjinBongkar> ConfirmIjinBongkar { get;  set; }
        public DbSet<PegawaiList> PegawaiList { get;  set; }
        public DbSet<NDBongkar> NDBongkar { get;  set; }
        public DbSet<Disposisi> Disposisi { get;  set; }
        public DbSet<LapTimbun> LapTimbun { get;  set; }
        public DbSet<PenggunaJasa> PenggunaJasa { get;  set; }
        public DbSet<Penempatan> Penempatan { get;  set; }
        public DbSet<ProgramKerja> ProgramKerja { get;  set; }
        public DbSet<Update> Update { get;  set; }
        
    }
}
