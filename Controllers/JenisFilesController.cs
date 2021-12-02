using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.ViewModel;

namespace ProjectAlpha.Controllers
{
    public class JenisFilesController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public JenisFilesController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: JenisFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.JenisFile.ToListAsync());
        }

        // GET: JenisFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisFile = await _context.JenisFile
                .FirstOrDefaultAsync(m => m.JenisFileID == id);
            if (jenisFile == null)
            {
                return NotFound();
            }

            return View(jenisFile);
        }

        // GET: JenisFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JenisFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JenisFileID,FileType")] JenisFile jenisFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jenisFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jenisFile);
        }

        // GET: JenisFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisFile = await _context.JenisFile.FindAsync(id);
            if (jenisFile == null)
            {
                return NotFound();
            }
            return View(jenisFile);
        }

        // POST: JenisFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JenisFileID,FileType")] JenisFile jenisFile)
        {
            if (id != jenisFile.JenisFileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jenisFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JenisFileExists(jenisFile.JenisFileID))
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
            return View(jenisFile);
        }

        // GET: JenisFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisFile = await _context.JenisFile
                .FirstOrDefaultAsync(m => m.JenisFileID == id);
            if (jenisFile == null)
            {
                return NotFound();
            }

            return View(jenisFile);
        }

        // POST: JenisFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jenisFile = await _context.JenisFile.FindAsync(id);
            _context.JenisFile.Remove(jenisFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JenisFileExists(int id)
        {
            return _context.JenisFile.Any(e => e.JenisFileID == id);
        }
    }
}
