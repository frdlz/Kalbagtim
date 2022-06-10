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
    public class JabatansController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public JabatansController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Jabatans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jabatan.ToListAsync());
        }

        // GET: Jabatans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jabatan = await _context.Jabatan
                .FirstOrDefaultAsync(m => m.JabatanID == id);
            if (jabatan == null)
            {
                return NotFound();
            }

            return View(jabatan);
        }

        // GET: Jabatans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jabatans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JabatanID,NamaJabatan")] Jabatan jabatan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jabatan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jabatan);
        }

        // GET: Jabatans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jabatan = await _context.Jabatan.FindAsync(id);
            if (jabatan == null)
            {
                return NotFound();
            }
            return View(jabatan);
        }

        // POST: Jabatans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("JabatanID,NamaJabatan")] Jabatan jabatan)
        {
            if (id != jabatan.JabatanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jabatan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JabatanExists(jabatan.JabatanID))
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
            return View(jabatan);
        }

        // GET: Jabatans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jabatan = await _context.Jabatan
                .FirstOrDefaultAsync(m => m.JabatanID == id);
            if (jabatan == null)
            {
                return NotFound();
            }

            return View(jabatan);
        }

        // POST: Jabatans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var jabatan = await _context.Jabatan.FindAsync(id);
            _context.Jabatan.Remove(jabatan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JabatanExists(string id)
        {
            return _context.Jabatan.Any(e => e.JabatanID == id);
        }
    }
}
