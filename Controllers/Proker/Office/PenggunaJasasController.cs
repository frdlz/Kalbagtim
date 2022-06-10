using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models;
using ProjectAlpha.Models.Proker.Office;

namespace ProjectAlpha.Controllers.Proker.Office
{
    public class PenggunaJasasController : Controller
    {
        private readonly ProjectAlphaContext _context;
        private UserManager<AppUser> userManager;

        public PenggunaJasasController(ProjectAlphaContext context, UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
            _context = context;
        }

        // GET: PenggunaJasas
        public async Task<IActionResult> Index()
        {
            return View(await _context.PenggunaJasa.ToListAsync());
        }

        // GET: PenggunaJasas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penggunaJasa = await _context.PenggunaJasa
                .FirstOrDefaultAsync(m => m.PenggunaJasaID == id);
            if (penggunaJasa == null)
            {
                return NotFound();
            }

            return View(penggunaJasa);
        }

        // GET: PenggunaJasas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PenggunaJasas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PenggunaJasaID,NamaPerusahaan,NPWP,Alamat,Kota")] PenggunaJasa penggunaJasa)
        {
            if (ModelState.IsValid)
            {
                penggunaJasa.PenggunaJasaID = userManager.GetUserId(User);

                _context.Add(penggunaJasa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(penggunaJasa);
        }

        // GET: PenggunaJasas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penggunaJasa = await _context.PenggunaJasa.FindAsync(id);
            if (penggunaJasa == null)
            {
                return NotFound();
            }
            return View(penggunaJasa);
        }

        // POST: PenggunaJasas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PenggunaJasaID,NamaPerusahaan,NPWP,Alamat,Kota")] PenggunaJasa penggunaJasa)
        {
            if (id != penggunaJasa.PenggunaJasaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penggunaJasa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenggunaJasaExists(penggunaJasa.PenggunaJasaID))
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
            return View(penggunaJasa);
        }

        // GET: PenggunaJasas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penggunaJasa = await _context.PenggunaJasa
                .FirstOrDefaultAsync(m => m.PenggunaJasaID == id);
            if (penggunaJasa == null)
            {
                return NotFound();
            }

            return View(penggunaJasa);
        }

        // POST: PenggunaJasas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var penggunaJasa = await _context.PenggunaJasa.FindAsync(id);
            _context.PenggunaJasa.Remove(penggunaJasa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenggunaJasaExists(string id)
        {
            return _context.PenggunaJasa.Any(e => e.PenggunaJasaID == id);
        }
    }
}
