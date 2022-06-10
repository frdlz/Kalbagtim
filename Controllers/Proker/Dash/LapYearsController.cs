using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Dash;

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class LapYearsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public LapYearsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: LapYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.LapYear.ToListAsync());
        }

        // GET: LapYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapYear = await _context.LapYear
                .FirstOrDefaultAsync(m => m.LapYearID == id);
            if (lapYear == null)
            {
                return NotFound();
            }

            return View(lapYear);
        }
       
       
        [HttpGet("AddTopLokasi/{Id}")]
        public async Task<IActionResult> AddTopLokasi(int? id)
        {
            var muatanlaut = new TopLokasi { LapYearID = id.Value };

            return View(muatanlaut);
        }

        [HttpPost("AddTopLokasi/{Id}")]
        public async Task<IActionResult> CreateAddTopLokasi([Bind("TopLokasiID,Bulan,Nama,JumlahPIB,BM,LapYearID")] TopLokasi muatanlautform)
        {

            if (ModelState.IsValid)
            {
                TopLokasi muatanlaut = new TopLokasi
                {
                    TopLokasiID = muatanlautform.TopLokasiID,
                    Bulan = muatanlautform.Bulan,
                    Nama = muatanlautform.Nama,
                    JumlahPIB = muatanlautform.JumlahPIB,
                    BM = muatanlautform.BM,
                    LapYearID = muatanlautform.LapYearID,

                };
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddTopLokasi", new TopLokasi
                {
                    TopLokasiID = muatanlaut.TopLokasiID,
                });
            }
            return View(muatanlautform);
        }
        // GET: LapYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LapYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LapYearID,Year")] LapYear lapYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lapYear);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lapYear);
        }

        // GET: LapYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapYear = await _context.LapYear.FindAsync(id);
            if (lapYear == null)
            {
                return NotFound();
            }
            return View(lapYear);
        }

        // POST: LapYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LapYearID,Year")] LapYear lapYear)
        {
            if (id != lapYear.LapYearID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lapYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LapYearExists(lapYear.LapYearID))
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
            return View(lapYear);
        }

        // GET: LapYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapYear = await _context.LapYear
                .FirstOrDefaultAsync(m => m.LapYearID == id);
            if (lapYear == null)
            {
                return NotFound();
            }

            return View(lapYear);
        }

        // POST: LapYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lapYear = await _context.LapYear.FindAsync(id);
            _context.LapYear.Remove(lapYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LapYearExists(int id)
        {
            return _context.LapYear.Any(e => e.LapYearID == id);
        }
    }
}
