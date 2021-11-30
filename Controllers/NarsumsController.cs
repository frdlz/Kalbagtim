using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models;

namespace ProjectAlpha.Controllers
{
    public class NarsumsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public NarsumsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Narsums
        public async Task<IActionResult> Index()
        {
            var narsum = _context.Narsum
                .AsNoTracking();
            return View(await narsum.ToListAsync());
        }

        // GET: Narsums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var p2kp = _context.P2kp;
            var narsum = await _context.Narsum
                .Include(a => a.P2Kps)
                
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.NarsumID == id);
            if (narsum == null)
            {
                return NotFound();
            }

            return View(narsum);
        }

        // GET: Narsums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Narsums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NarsumID,Narasumber,Keterangan")] Narsum narsum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narsum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(narsum);
        }

        // GET: Narsums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narsum = await _context.Narsum.FindAsync(id);
            if (narsum == null)
            {
                return NotFound();
            }
            return View(narsum);
        }

        // POST: Narsums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NarsumID,Narasumber,Keterangan")] Narsum narsum)
        {
            if (id != narsum.NarsumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narsum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarsumExists(narsum.NarsumID))
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
            return View(narsum);
        }

        // GET: Narsums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narsum = await _context.Narsum
                .FirstOrDefaultAsync(m => m.NarsumID == id);
            if (narsum == null)
            {
                return NotFound();
            }

            return View(narsum);
        }

        // POST: Narsums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narsum = await _context.Narsum.FindAsync(id);
            _context.Narsum.Remove(narsum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarsumExists(int id)
        {
            return _context.Narsum.Any(e => e.NarsumID == id);
        }
    }
}
