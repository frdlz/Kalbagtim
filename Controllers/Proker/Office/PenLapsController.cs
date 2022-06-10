using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Office;
using ProjectAlpha.Models.Proker.Office.ViewModel;

namespace ProjectAlpha.Controllers.Proker.Office
{
    public class PenLapsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public PenLapsController(ProjectAlphaContext context)
        {
            _context = context;
        }
        public IActionResult Print2()
        {

            return View();
        }
        public async Task<IActionResult> Print(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var penlap = await _context.PenLap
                         .Include(a => a.Bongkar)
                         .ThenInclude(a => a.PenggunaJasa)
                         
                         .Include(a => a.DaftarLokasiPerijinan)
                         .FirstOrDefaultAsync(a => a.PenlapID == id)
                         ;
            


            if (penlap == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(penlap)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
                ;

        }
        public async Task<IActionResult> Index()
        {
            BongkarViewModel model = new BongkarViewModel();

            var bongkar = from a in _context.Bongkar
                          .Include(a => a.PenggunaJasa)
                          select a;
            var barang = from a in _context.PosBarang
                         select a;
            var lokasi = from a in _context.DaftarLokasiPerijinan
                         select a;
            var penlap = from a in _context.PenLap
                         .Include(a => a.Bongkar)
                         
                         .Include(a => a.DaftarLokasiPerijinan)
                         select a;
            
                         
                        
            var selectBongkar = new BongkarViewModel
            {
                bongkars = await bongkar.ToListAsync(),
                daftarLokasiPerijinans = await lokasi.ToListAsync(),
                penLaps = await penlap.ToListAsync(),
                posBarangs = await barang.ToListAsync(),
               


            };
            return View(selectBongkar);
        }
        // GET: PenLaps
        public async Task<IActionResult> ff()
        {
            var ProjectAlphaContext = _context.PenLap
                .Include(p => p.DaftarLokasiPerijinan)
                .ThenInclude(a => a.Bongkars)
                .ThenInclude(a => a.PenggunaJasa);
            
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: PenLaps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penLap = await _context.PenLap
                .Include(p => p.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.PenlapID == id);
            if (penLap == null)
            {
                return NotFound();
            }

            return View(penLap);
        }

        // GET: PenLaps/Create
        public IActionResult Create()
        {
            PopulateNomorDropdownList();
            PopulatePetugasP21DropdownList();
            PopulatePetugasP22DropdownList();
            PopulateKasiDropdownList();
            PopulateLokasiDropdownList();
            return View();
        }

        // POST: PenLaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PenlapID,NoPermohonan,Petugas1,Petugas2,Kasi,NoBA,TanggalBA,DaftarLokasiPerijinanID")] PenLap penLap)
        {
            if (ModelState.IsValid)
            {
                PopulateNomorDropdownList(ViewBag.NoPermohonan);
                PopulatePetugasP21DropdownList(ViewBag.Petugas1);
                PopulatePetugasP22DropdownList(ViewBag.Petugas2);
                PopulateKasiDropdownList(ViewBag.Kasi);
                PopulateLokasiDropdownList(ViewBag.NamaLokasi);
                _context.Add(penLap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(penLap);
        }

        // GET: PenLaps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penLap = await _context.PenLap.FindAsync(id);
            if (penLap == null)
            {
                return NotFound();
            }
            
            return View(penLap);
        }

        // POST: PenLaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PenlapID,NoBA,TanggalBA,DaftarLokasiPerijinanID")] PenLap penLap)
        {
            if (id != penLap.PenlapID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penLap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenLapExists(penLap.PenlapID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(penLap);
        }

        // GET: PenLaps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penLap = await _context.PenLap
                .Include(p => p.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.PenlapID == id);
            if (penLap == null)
            {
                return NotFound();
            }

            return View(penLap);
        }

        // POST: PenLaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var penLap = await _context.PenLap.FindAsync(id);
            _context.PenLap.Remove(penLap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenLapExists(string id)
        {
            return _context.PenLap.Any(e => e.PenlapID == id);
        }
        private void PopulateLokasiDropdownList(object selectedPendok = null)
        {
            var pendoksQuery = from d in _context.DaftarLokasiPerijinan
                               orderby d.NamaLokasi
                               select d;
            ViewBag.NamaLokasi = new SelectList(pendoksQuery.AsNoTracking(), "DaftarLokasiPerijinanID", "NamaLokasi", selectedPendok);
        }
        private void PopulateNomorDropdownList(object selectedPendok = null)
        {
            var pendoksQuery = from d in _context.Bongkar
                               orderby d.NoPermohonan
                               select d;
            ViewBag.NoPermohonan = new SelectList(pendoksQuery.AsNoTracking(), "NoPermohonan", "NoPermohonan", selectedPendok);
        }
        private void PopulatePetugasP21DropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PegawaiList
                           orderby d.Nama
                           select d;
            ViewBag.Petugas1 = new SelectList(p2sQuery

                .AsNoTracking(), "PegawaiListID", "Nama", selectedPetugasP2);
        }
        private void PopulatePetugasP22DropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PegawaiList
                           orderby d.Nama
                           select d;
            ViewBag.Petugas2 = new SelectList(p2sQuery

                .AsNoTracking(), "PegawaiListID", "Nama", selectedPetugasP2);
        }
        private void PopulateKasiDropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PegawaiList
                           orderby d.Nama
                           select d;
            ViewBag.Kasi = new SelectList(p2sQuery

                .AsNoTracking(), "PegawaiListID", "Nama", selectedPetugasP2);
        }
    }
}
