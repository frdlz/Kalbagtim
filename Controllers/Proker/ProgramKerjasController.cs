using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker;
using ProjectAlpha.Models.Proker.ProkerViewModel;

namespace ProjectAlpha.Controllers.Proker
{
    public class ProgramKerjasController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public ProgramKerjasController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: ProgramKerjas
        public async Task<IActionResult> Dashboard()
        {
            IndexViewModel model = new IndexViewModel();
            model.ProgramKerjas = _context.ProgramKerja;
            model.Updates = _context.Update.Where(a => a.Status == StatusUpdate.create)
                .Include(a => a.Program)
                .OrderByDescending(a => a.Tanggal);
           
            model.TotalProgram = _context.ProgramKerja.Count();
            model.TotalProgress = _context.Update.Where(a => a.Status == StatusUpdate.create).Count();
            
            model.TotalRapat = _context.ProgramKerja.Count();
            model.TotalPegawai = _context.ProgramKerja.Count();
            return View(model);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgramKerja.ToListAsync());
        }
        // GET: ProgramKerjas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programKerja = await _context.ProgramKerja
                .FirstOrDefaultAsync(m => m.ProgramID == id);
            if (programKerja == null)
            {
                return NotFound();
            }

            return View(programKerja);
        }
        [HttpGet("Update/{Id}")]
        public async Task<IActionResult> Update(string id)
        {
            var update = new Update { ProgramID = id };

            return View(update);
        }

        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> CreateUpdate([Bind("UpdateID,Keterangan,ProgramID,Tanggal,Status")] Update updateform)
        {
            var xcv = string.Format(@"{0}", Guid.NewGuid());
            if (ModelState.IsValid)
            {
                Update update = new Update
                {
                    UpdateID = updateform.UpdateID,
                    Keterangan = updateform.Keterangan,
                    ProgramID = updateform.ProgramID,
                    Tanggal = updateform.Tanggal,
                    Status = StatusUpdate.create,
                     
                };
                _context.Add(update);
                await _context.SaveChangesAsync();

                return View("Update", new Update
                {
                    ProgramID = update.ProgramID,
                });
            }
            return View(updateform);
        }
        // GET: ProgramKerjas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgramKerjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramID,NamaProgram,Keterangan,Status")] ProgramKerja programKerja)
        {
            if (ModelState.IsValid)
            {
                programKerja.Status = StatusProgram.create;
                _context.Add(programKerja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programKerja);
        }

        // GET: ProgramKerjas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programKerja = await _context.ProgramKerja.FindAsync(id);
            if (programKerja == null)
            {
                return NotFound();
            }
            return View(programKerja);
        }

        // POST: ProgramKerjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProgramID,NamaProgram,Keterangan,Status")] ProgramKerja programKerja)
        {
            if (id != programKerja.ProgramID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programKerja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramKerjaExists(programKerja.ProgramID))
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
            return View(programKerja);
        }

        // GET: ProgramKerjas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programKerja = await _context.ProgramKerja
                .FirstOrDefaultAsync(m => m.ProgramID == id);
            if (programKerja == null)
            {
                return NotFound();
            }

            return View(programKerja);
        }

        // POST: ProgramKerjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var programKerja = await _context.ProgramKerja.FindAsync(id);
            _context.ProgramKerja.Remove(programKerja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramKerjaExists(string id)
        {
            return _context.ProgramKerja.Any(e => e.ProgramID == id);
        }
    }
}
