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
    public class DaftarLokasiPerijinansController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public DaftarLokasiPerijinansController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: DaftarLokasiPerijinans
        public async Task<IActionResult> Index()
        {
            BongkarViewModel model = new BongkarViewModel();

            var bongkar = from a in _context.Bongkar
                          .Where(a => a.Status == StatusBongkar.create)
                          .Include(a => a.PenggunaJasa)
                          select a;
            var barang = from a in _context.PosBarang
                         select a;
            var lokasi = from a in _context.DaftarLokasiPerijinan
                         select a;
            var penlap = from a in _context.PenLap
                         select a;
            var selectBongkar = new BongkarViewModel
            {
                bongkars = await bongkar.ToListAsync(),
                daftarLokasiPerijinans = await lokasi.ToListAsync(),
                penLaps = await penlap.ToListAsync(),
                posBarangs = await barang.ToListAsync()


            };
            return View(selectBongkar);
        }

        // GET: DaftarLokasiPerijinans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var namalok = _context.PenLap;
            var daftarLokasiPerijinan = await _context.DaftarLokasiPerijinan
               
                .Include(a => a.PenLaps)
                .ThenInclude(a => a.Bongkar)
                
                 .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DaftarLokasiPerijinanID == id);
            if (daftarLokasiPerijinan == null)
            {
                return NotFound();
            }

            return View(daftarLokasiPerijinan);
        }

        // GET: DaftarLokasiPerijinans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DaftarLokasiPerijinans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DaftarLokasiPerijinanID,NamaLokasi,Alamat,Lat,Long,Pengelola")] DaftarLokasiPerijinan daftarLokasiPerijinan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daftarLokasiPerijinan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(daftarLokasiPerijinan);
        }

        // GET: DaftarLokasiPerijinans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daftarLokasiPerijinan = await _context.DaftarLokasiPerijinan.FindAsync(id);
            if (daftarLokasiPerijinan == null)
            {
                return NotFound();
            }
            return View(daftarLokasiPerijinan);
        }

        // POST: DaftarLokasiPerijinans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DaftarLokasiPerijinanID,NamaLokasi,Alamat,Lat,Long,Pengelola")] DaftarLokasiPerijinan daftarLokasiPerijinan)
        {
            if (id != daftarLokasiPerijinan.DaftarLokasiPerijinanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daftarLokasiPerijinan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaftarLokasiPerijinanExists(daftarLokasiPerijinan.DaftarLokasiPerijinanID))
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
            return View(daftarLokasiPerijinan);
        }

        // GET: DaftarLokasiPerijinans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daftarLokasiPerijinan = await _context.DaftarLokasiPerijinan
                .FirstOrDefaultAsync(m => m.DaftarLokasiPerijinanID == id);
            if (daftarLokasiPerijinan == null)
            {
                return NotFound();
            }

            return View(daftarLokasiPerijinan);
        }

        // POST: DaftarLokasiPerijinans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var daftarLokasiPerijinan = await _context.DaftarLokasiPerijinan.FindAsync(id);
            _context.DaftarLokasiPerijinan.Remove(daftarLokasiPerijinan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaftarLokasiPerijinanExists(string id)
        {
            return _context.DaftarLokasiPerijinan.Any(e => e.DaftarLokasiPerijinanID == id);
        }
    }
}
