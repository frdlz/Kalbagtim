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
    public class DisposisisController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public DisposisisController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Disposisis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disposisi.ToListAsync());
        }

        // GET: Disposisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disposisi = await _context.Disposisi
                .FirstOrDefaultAsync(m => m.DisposisiID == id);
            if (disposisi == null)
            {
                return NotFound();
            }

            return View(disposisi);
        }

        // GET: Disposisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disposisis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisposisiID,DisposisiName,BongkarID")] Disposisi disposisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disposisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disposisi);
        }

        // GET: Disposisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disposisi = await _context.Disposisi.FindAsync(id);
            if (disposisi == null)
            {
                return NotFound();
            }
            return View(disposisi);
        }

        // POST: Disposisis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisposisiID,DisposisiName,BongkarID")] Disposisi disposisi)
        {
            if (id != disposisi.DisposisiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disposisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisposisiExists(disposisi.DisposisiID))
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
            return View(disposisi);
        }

        // GET: Disposisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disposisi = await _context.Disposisi
                .FirstOrDefaultAsync(m => m.DisposisiID == id);
            if (disposisi == null)
            {
                return NotFound();
            }

            return View(disposisi);
        }

        // POST: Disposisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disposisi = await _context.Disposisi.FindAsync(id);
            _context.Disposisi.Remove(disposisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisposisiExists(int id)
        {
            return _context.Disposisi.Any(e => e.DisposisiID == id);
        }
    }
}
