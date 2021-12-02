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
    public class P2kpController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public P2kpController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: P2kp
        public async Task<IActionResult> Index()
        {
            var p2kp = _context.P2kp
                .Include(a => a.Narsum)
                .AsNoTracking();
            return View(await p2kp.ToListAsync());
        }

        // GET: P2kp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p2kp = await _context.P2kp
                .Include(a => a.Narsum)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.P2kpID == id);
            if (p2kp == null)
            {
                return NotFound();
            }

            return View(p2kp);
        }

        // GET: P2kp/Create
        public IActionResult Create()
        {
            PopulateNarasumberDropdownList();
            return View();
            
        }

        // POST: P2kp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("P2kpID,Judul,Tanggal,JamMulai,JamSelesai,Tempat,NarsumID,Status,WaktuBuat")] P2kp p2kp)
        {
            if (ModelState.IsValid)
            {
                P2kp latihan = new P2kp
                {
                    P2kpID = p2kp.P2kpID,
                    Judul = p2kp.Judul,
                    Tanggal = p2kp.Tanggal,
                    JamMulai = p2kp.JamMulai,
                    JamSelesai = p2kp.JamSelesai,
                    Tempat = p2kp.Tempat,
                    NarsumID = p2kp.NarsumID,
                    Status = StatusP2kp.belum,
                    WaktuBuat = DateTime.Now
                    
                };
                PopulateNarasumberDropdownList(ViewBag.NarsumID);
                _context.Add(p2kp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(p2kp);
        }
        public async Task<IActionResult> Selesai(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p2kp = await _context.P2kp

                .FirstOrDefaultAsync(a => a.P2kpID == id);

            if (p2kp == null)
            {
                return NotFound();
            }
            PopulateNarasumberDropdownList(p2kp.NarsumID);
            return View(p2kp);
        }

        // POST: P2kp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Selesai")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WaktuSelesai(int id, [Bind("P2kpID,Judul,Tanggal,JamMulai,JamSelesai,Tempat,NarsumID,Status,WaktuBuat")] P2kp p2kp)
        {
            if (id != p2kp.P2kpID)
            {
                return NotFound();
            }
            var statusToUpdate = await _context.P2kp
                .FirstOrDefaultAsync(a => a.P2kpID == id);
            if (await TryUpdateModelAsync<P2kp>(statusToUpdate,
                "", a => a.WaktuSelesai, a => a.Status))
            {
                try
                {
                    statusToUpdate.WaktuSelesai = DateTime.Now;
                    statusToUpdate.Status = StatusP2kp.selesai;
                     
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Tidak bisa merubah data." +
                        "Silahkan hubungi Admin.");
                }
            }
            PopulateNarasumberDropdownList(statusToUpdate.NarsumID);
            return View(statusToUpdate);
        }

        // GET: P2kp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p2kp = await _context.P2kp

                .FirstOrDefaultAsync(a => a.P2kpID == id);
                
            if (p2kp == null)
            {
                return NotFound();
            }
            PopulateNarasumberDropdownList(p2kp.NarsumID);
            return View(p2kp);
        }

        // POST: P2kp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditP2kp(int id, [Bind("P2kpID,Judul,Tanggal,JamMulai,JamSelesai,Tempat,NarsumID,Status,WaktuBuat")] P2kp p2kp)
        {
            if (id != p2kp.P2kpID)
            {
                return NotFound();
            }
            var p2kpToUpdate = await _context.P2kp
                .FirstOrDefaultAsync(a => a.P2kpID == id);
            if (await TryUpdateModelAsync<P2kp>(p2kpToUpdate,
                "", a => a.Judul, a => a.Tanggal,a => a.JamMulai,a => a.JamSelesai,a => a.Tempat, a => a.NarsumID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Tidak bisa merubah data." +
                        "Silahkan hubungi Admin.");
                }
            }
            PopulateNarasumberDropdownList(p2kpToUpdate.NarsumID);
            return View(p2kpToUpdate);
        }

        // GET: P2kp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p2kp = await _context.P2kp
                .Include(a => a.Narsum)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.P2kpID == id);
            if (p2kp == null)
            {
                return NotFound();
            }

            return View(p2kp);
        }

        // POST: P2kp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p2kp = await _context.P2kp.FindAsync(id);
            _context.P2kp.Remove(p2kp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool P2kpExists(int id)
        {
            return _context.P2kp.Any(e => e.P2kpID == id);
        }

        private void PopulateNarasumberDropdownList(object selectedNarsum = null)
        {
            var narsumsQuery = from d in _context.Narsum
                               orderby d.NarsumID
                               select d;
            ViewBag.NarsumID = new SelectList(narsumsQuery.AsNoTracking(), "NarsumID", "Narasumber", selectedNarsum);
        }
    }
}
