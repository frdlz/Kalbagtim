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
    public class PangkatGolsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public PangkatGolsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: PangkatGols
        public async Task<IActionResult> Index()
        {
            return View(await _context.PangkatGol.ToListAsync());
        }

        // GET: PangkatGols/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pangkatGol = await _context.PangkatGol
                .FirstOrDefaultAsync(m => m.PangkatGolID == id);
            if (pangkatGol == null)
            {
                return NotFound();
            }

            return View(pangkatGol);
        }

        // GET: PangkatGols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PangkatGols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PangkatGolID,NamaPangkat")] PangkatGol pangkatGol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pangkatGol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pangkatGol);
        }

        // GET: PangkatGols/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pangkatGol = await _context.PangkatGol.FindAsync(id);
            if (pangkatGol == null)
            {
                return NotFound();
            }
            return View(pangkatGol);
        }

        // POST: PangkatGols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PangkatGolID,NamaPangkat")] PangkatGol pangkatGol)
        {
            if (id != pangkatGol.PangkatGolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pangkatGol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PangkatGolExists(pangkatGol.PangkatGolID))
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
            return View(pangkatGol);
        }

        // GET: PangkatGols/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pangkatGol = await _context.PangkatGol
                .FirstOrDefaultAsync(m => m.PangkatGolID == id);
            if (pangkatGol == null)
            {
                return NotFound();
            }

            return View(pangkatGol);
        }

        // POST: PangkatGols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pangkatGol = await _context.PangkatGol.FindAsync(id);
            _context.PangkatGol.Remove(pangkatGol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PangkatGolExists(string id)
        {
            return _context.PangkatGol.Any(e => e.PangkatGolID == id);
        }
    }
}
