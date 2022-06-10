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
    public class PenempatansController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public PenempatansController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Penempatans
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.Penempatan.Include(p => p.DaftarLokasi).Include(p => p.ListPegawai);
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: Penempatans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penempatan = await _context.Penempatan
                .Include(p => p.DaftarLokasi)
                .Include(p => p.ListPegawai)
                .FirstOrDefaultAsync(m => m.PenempatanID == id);
            if (penempatan == null)
            {
                return NotFound();
            }

            return View(penempatan);
        }

        // GET: Penempatans/Create
        public IActionResult Create()
        {
            ViewData["DaftarLokasiID"] = new SelectList(_context.DaftarLokasi, "DaftarLokasiID", "DaftarLokasiID");
            ViewData["ListPegawaiID"] = new SelectList(_context.ListPegawai, "ListPegawaiID", "ListPegawaiID");
            return View();
        }

        // POST: Penempatans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PenempatanID,Mulai,Selesai,ListPegawaiID,DaftarLokasiID")] Penempatan penempatan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penempatan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DaftarLokasiID"] = new SelectList(_context.DaftarLokasi, "DaftarLokasiID", "DaftarLokasiID", penempatan.DaftarLokasiID);
            ViewData["ListPegawaiID"] = new SelectList(_context.ListPegawai, "ListPegawaiID", "ListPegawaiID", penempatan.ListPegawaiID);
            return View(penempatan);
        }

        // GET: Penempatans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penempatan = await _context.Penempatan.FindAsync(id);
            if (penempatan == null)
            {
                return NotFound();
            }
            ViewData["DaftarLokasiID"] = new SelectList(_context.DaftarLokasi, "DaftarLokasiID", "DaftarLokasiID", penempatan.DaftarLokasiID);
            ViewData["ListPegawaiID"] = new SelectList(_context.ListPegawai, "ListPegawaiID", "ListPegawaiID", penempatan.ListPegawaiID);
            return View(penempatan);
        }

        // POST: Penempatans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PenempatanID,Mulai,Selesai,ListPegawaiID,DaftarLokasiID")] Penempatan penempatan)
        {
            if (id != penempatan.PenempatanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penempatan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenempatanExists(penempatan.PenempatanID))
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
            ViewData["DaftarLokasiID"] = new SelectList(_context.DaftarLokasi, "DaftarLokasiID", "DaftarLokasiID", penempatan.DaftarLokasiID);
            ViewData["ListPegawaiID"] = new SelectList(_context.ListPegawai, "ListPegawaiID", "ListPegawaiID", penempatan.ListPegawaiID);
            return View(penempatan);
        }

        // GET: Penempatans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penempatan = await _context.Penempatan
                .Include(p => p.DaftarLokasi)
                .Include(p => p.ListPegawai)
                .FirstOrDefaultAsync(m => m.PenempatanID == id);
            if (penempatan == null)
            {
                return NotFound();
            }

            return View(penempatan);
        }

        // POST: Penempatans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var penempatan = await _context.Penempatan.FindAsync(id);
            _context.Penempatan.Remove(penempatan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenempatanExists(string id)
        {
            return _context.Penempatan.Any(e => e.PenempatanID == id);
        }
    }
}
