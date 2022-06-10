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
    public class PegawaiListsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public PegawaiListsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: PegawaiLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.PegawaiList.ToListAsync());
        }

        // GET: PegawaiLists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawaiList = await _context.PegawaiList
                .FirstOrDefaultAsync(m => m.PegawaiListID == id);
            if (pegawaiList == null)
            {
                return NotFound();
            }

            return View(pegawaiList);
        }

        // GET: PegawaiLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PegawaiLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PegawaiListID,Nama,Pangkat,Golongan,Jabatan,Penempatan")] PegawaiList pegawaiList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pegawaiList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pegawaiList);
        }

        // GET: PegawaiLists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawaiList = await _context.PegawaiList.FindAsync(id);
            if (pegawaiList == null)
            {
                return NotFound();
            }
            return View(pegawaiList);
        }

        // POST: PegawaiLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PegawaiListID,Nama,Pangkat,Golongan,Jabatan,Penempatan")] PegawaiList pegawaiList)
        {
            if (id != pegawaiList.PegawaiListID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pegawaiList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PegawaiListExists(pegawaiList.PegawaiListID))
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
            return View(pegawaiList);
        }

        // GET: PegawaiLists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawaiList = await _context.PegawaiList
                .FirstOrDefaultAsync(m => m.PegawaiListID == id);
            if (pegawaiList == null)
            {
                return NotFound();
            }

            return View(pegawaiList);
        }

        // POST: PegawaiLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var pegawaiList = await _context.PegawaiList.FindAsync(id);
            _context.PegawaiList.Remove(pegawaiList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PegawaiListExists(string id)
        {
            return _context.PegawaiList.Any(e => e.PegawaiListID == id);
        }
    }
}
