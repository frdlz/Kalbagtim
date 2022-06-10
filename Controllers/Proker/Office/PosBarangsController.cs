using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Office;

namespace ProjectAlpha.Controllers.Proker.Office
{
    public class PosBarangsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public PosBarangsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: PosBarangs
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.PosBarang.Include(p => p.Bongkar);
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: PosBarangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posBarang = await _context.PosBarang
                .Include(p => p.Bongkar)
                .FirstOrDefaultAsync(m => m.PosBarangID == id);
            if (posBarang == null)
            {
                return NotFound();
            }

            return View(posBarang);
        }

        // GET: PosBarangs/Create
        public IActionResult Create()
        {
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID");
            return View();
        }

        // POST: PosBarangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PosBarangID,NoPos,BL,Jumlah,JenisKemasan,UraianBarang,BongkarID")] PosBarang posBarang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posBarang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", posBarang.BongkarID);
            return View(posBarang);
        }

        // GET: PosBarangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posBarang = await _context.PosBarang.FindAsync(id);
            if (posBarang == null)
            {
                return NotFound();
            }
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", posBarang.BongkarID);
            return View(posBarang);
        }

        // POST: PosBarangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PosBarangID,NoPos,BL,Jumlah,JenisKemasan,UraianBarang,BongkarID")] PosBarang posBarang)
        {
            if (id != posBarang.PosBarangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posBarang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosBarangExists(posBarang.PosBarangID))
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
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", posBarang.BongkarID);
            return View(posBarang);
        }

        // GET: PosBarangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posBarang = await _context.PosBarang
                .Include(p => p.Bongkar)
                .FirstOrDefaultAsync(m => m.PosBarangID == id);
            if (posBarang == null)
            {
                return NotFound();
            }

            return View(posBarang);
        }

        // POST: PosBarangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var posBarang = await _context.PosBarang.FindAsync(id);
            _context.PosBarang.Remove(posBarang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosBarangExists(string id)
        {
            return _context.PosBarang.Any(e => e.PosBarangID == id);
        }
    }
}
