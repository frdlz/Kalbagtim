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
    public class ConfirmIjinBongkarsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public ConfirmIjinBongkarsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: ConfirmIjinBongkars
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.ConfirmIjinBongkar.Include(c => c.Bongkar);
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: ConfirmIjinBongkars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmIjinBongkar = await _context.ConfirmIjinBongkar
                .Include(c => c.Bongkar)
                .FirstOrDefaultAsync(m => m.ConfirmIjinBongkarID == id);
            if (confirmIjinBongkar == null)
            {
                return NotFound();
            }

            return View(confirmIjinBongkar);
        }

        // GET: ConfirmIjinBongkars/Create
        public IActionResult Create()
        {
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID");
            return View();
        }

        // POST: ConfirmIjinBongkars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConfirmIjinBongkarID,JenisDok,NomorDokumen,TanggalDok,StatusDok,BongkarID")] ConfirmIjinBongkar confirmIjinBongkar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(confirmIjinBongkar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", confirmIjinBongkar.BongkarID);
            return View(confirmIjinBongkar);
        }

        // GET: ConfirmIjinBongkars/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmIjinBongkar = await _context.ConfirmIjinBongkar.FindAsync(id);
            if (confirmIjinBongkar == null)
            {
                return NotFound();
            }
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", confirmIjinBongkar.BongkarID);
            return View(confirmIjinBongkar);
        }

        // POST: ConfirmIjinBongkars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConfirmIjinBongkarID,NomorDokumen,TanggalDok,StatusDok,BongkarID")] ConfirmIjinBongkar confirmIjinBongkar)
        {
            if (id != confirmIjinBongkar.ConfirmIjinBongkarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(confirmIjinBongkar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfirmIjinBongkarExists(confirmIjinBongkar.ConfirmIjinBongkarID))
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
            ViewData["BongkarID"] = new SelectList(_context.Bongkar, "BongkarID", "BongkarID", confirmIjinBongkar.BongkarID);
            return View(confirmIjinBongkar);
        }

        // GET: ConfirmIjinBongkars/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmIjinBongkar = await _context.ConfirmIjinBongkar
                .Include(c => c.Bongkar)
                .FirstOrDefaultAsync(m => m.ConfirmIjinBongkarID == id);
            if (confirmIjinBongkar == null)
            {
                return NotFound();
            }

            return View(confirmIjinBongkar);
        }

        // POST: ConfirmIjinBongkars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var confirmIjinBongkar = await _context.ConfirmIjinBongkar.FindAsync(id);
            _context.ConfirmIjinBongkar.Remove(confirmIjinBongkar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfirmIjinBongkarExists(string id)
        {
            return _context.ConfirmIjinBongkar.Any(e => e.ConfirmIjinBongkarID == id);
        }
    }
}
