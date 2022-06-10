using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker;

namespace ProjectAlpha.Controllers.Proker
{
    public class UpdatesController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public UpdatesController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Updates
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.Update.Include(u => u.Program);
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: Updates/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var update = await _context.Update
                .Include(u => u.Program)
                .FirstOrDefaultAsync(m => m.UpdateID == id);
            if (update == null)
            {
                return NotFound();
            }

            return View(update);
        }

        // GET: Updates/Create
        public IActionResult Create()
        {
            ViewData["ProgramID"] = new SelectList(_context.ProgramKerja, "ProgramID", "ProgramID");
            return View();
        }

        // POST: Updates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UpdateID,ProgramID,Keterangan,Tanggal,Status")] Update update)
        {
            if (ModelState.IsValid)
            {
                _context.Add(update);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramID"] = new SelectList(_context.ProgramKerja, "ProgramID", "ProgramID", update.ProgramID);
            return View(update);
        }

        // GET: Updates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var update = await _context.Update.FindAsync(id);
            if (update == null)
            {
                return NotFound();
            }
            ViewData["ProgramID"] = new SelectList(_context.ProgramKerja, "ProgramID", "ProgramID", update.ProgramID);
            return View(update);
        }

        // POST: Updates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UpdateID,ProgramID,Keterangan,Tanggal,Status")] Update update)
        {
            if (id != update.UpdateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(update);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpdateExists(update.UpdateID))
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
            ViewData["ProgramID"] = new SelectList(_context.ProgramKerja, "ProgramID", "ProgramID", update.ProgramID);
            return View(update);
        }

        // GET: Updates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var update = await _context.Update
                .Include(u => u.Program)
                .FirstOrDefaultAsync(m => m.UpdateID == id);
            if (update == null)
            {
                return NotFound();
            }

            return View(update);
        }

        // POST: Updates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var update = await _context.Update.FindAsync(id);
            _context.Update.Remove(update);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpdateExists(string id)
        {
            return _context.Update.Any(e => e.UpdateID == id);
        }
    }
}
