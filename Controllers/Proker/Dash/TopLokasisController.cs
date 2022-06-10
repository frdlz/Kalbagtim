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
    public class TopLokasisController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public TopLokasisController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: TopLokasis
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.TopLokasi.Include(t => t.LapYear);
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: TopLokasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topLokasi = await _context.TopLokasi
                .Include(t => t.LapYear)
                .FirstOrDefaultAsync(m => m.TopLokasiID == id);
            if (topLokasi == null)
            {
                return NotFound();
            }

            return View(topLokasi);
        }

        // GET: TopLokasis/Create
        public IActionResult Create()
        {
            ViewData["LapYearID"] = new SelectList(_context.LapYear, "LapYearID", "LapYearID");
            return View();
        }

        // POST: TopLokasis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopLokasiID,Bulan,Nama,JumlahPIB,BM,LapYearID")] TopLokasi topLokasi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topLokasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LapYearID"] = new SelectList(_context.LapYear, "LapYearID", "LapYearID", topLokasi.LapYearID);
            return View(topLokasi);
        }

        // GET: TopLokasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topLokasi = await _context.TopLokasi.FindAsync(id);
            if (topLokasi == null)
            {
                return NotFound();
            }
            ViewData["LapYearID"] = new SelectList(_context.LapYear, "LapYearID", "LapYearID", topLokasi.LapYearID);
            return View(topLokasi);
        }

        // POST: TopLokasis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopLokasiID,Bulan,Nama,JumlahPIB,BM,LapYearID")] TopLokasi topLokasi)
        {
            if (id != topLokasi.TopLokasiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topLokasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopLokasiExists(topLokasi.TopLokasiID))
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
            ViewData["LapYearID"] = new SelectList(_context.LapYear, "LapYearID", "LapYearID", topLokasi.LapYearID);
            return View(topLokasi);
        }

        // GET: TopLokasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topLokasi = await _context.TopLokasi
                .Include(t => t.LapYear)
                .FirstOrDefaultAsync(m => m.TopLokasiID == id);
            if (topLokasi == null)
            {
                return NotFound();
            }

            return View(topLokasi);
        }

        // POST: TopLokasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topLokasi = await _context.TopLokasi.FindAsync(id);
            _context.TopLokasi.Remove(topLokasi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopLokasiExists(int id)
        {
            return _context.TopLokasi.Any(e => e.TopLokasiID == id);
        }
    }
}
