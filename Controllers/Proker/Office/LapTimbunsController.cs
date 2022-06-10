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
    public class LapTimbunsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public LapTimbunsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: LapTimbuns
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.LapTimbun.Include(l => l.Bongkar);
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: LapTimbuns/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapTimbun = await _context.LapTimbun
                .Include(l => l.Bongkar)
                .FirstOrDefaultAsync(m => m.LapTimbunID == id);
            if (lapTimbun == null)
            {
                return NotFound();
            }

            return View(lapTimbun);
        }

        // GET: LapTimbuns/Create
        public IActionResult Create()
        {
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID");
            return View();
        }

        // POST: LapTimbuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LapTimbunID,NomorLap,Tanggal,BongkarID,Keterangan,NamaPejabat")] LapTimbun lapTimbun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lapTimbun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", lapTimbun.BongkarID);
            return View(lapTimbun);
        }

        // GET: LapTimbuns/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapTimbun = await _context.LapTimbun.FindAsync(id);
            if (lapTimbun == null)
            {
                return NotFound();
            }
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", lapTimbun.BongkarID);
            return View(lapTimbun);
        }

        // POST: LapTimbuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LapTimbunID,NomorLap,Tanggal,BongkarID,Keterangan,NamaPejabat")] LapTimbun lapTimbun)
        {
            if (id != lapTimbun.LapTimbunID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lapTimbun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LapTimbunExists(lapTimbun.LapTimbunID))
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
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", lapTimbun.BongkarID);
            return View(lapTimbun);
        }

        // GET: LapTimbuns/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapTimbun = await _context.LapTimbun
                .Include(l => l.Bongkar)
                .FirstOrDefaultAsync(m => m.LapTimbunID == id);
            if (lapTimbun == null)
            {
                return NotFound();
            }

            return View(lapTimbun);
        }

        // POST: LapTimbuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lapTimbun = await _context.LapTimbun.FindAsync(id);
            _context.LapTimbun.Remove(lapTimbun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LapTimbunExists(string id)
        {
            return _context.LapTimbun.Any(e => e.LapTimbunID == id);
        }
    }
}
