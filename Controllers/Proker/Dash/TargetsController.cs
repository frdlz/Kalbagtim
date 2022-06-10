using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Dash;

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class TargetsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public TargetsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Targets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Target.ToListAsync());
        }

        // GET: Targets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var target = await _context.Target
                .FirstOrDefaultAsync(m => m.TargetID == id);
            if (target == null)
            {
                return NotFound();
            }

            return View(target);
        }

        // GET: Targets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Targets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TargetID,TahunTarget,BM,BMMonthly,BK,BKMonthly,Cukai,CukaiMonthly,Total")] Target target)
        {
            if (ModelState.IsValid)
            {
                _context.Add(target);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(target);
        }

        // GET: Targets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var target = await _context.Target.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }
            return View(target);
        }

        // POST: Targets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TargetID,TahunTarget,BM,BMMonthly,BK,BKMonthly,Cukai,CukaiMonthly,Total")] Target target)
        {
            if (id != target.TargetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(target);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TargetExists(target.TargetID))
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
            return View(target);
        }

        // GET: Targets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var target = await _context.Target
                .FirstOrDefaultAsync(m => m.TargetID == id);
            if (target == null)
            {
                return NotFound();
            }

            return View(target);
        }

        // POST: Targets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var target = await _context.Target.FindAsync(id);
            _context.Target.Remove(target);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TargetExists(string id)
        {
            return _context.Target.Any(e => e.TargetID == id);
        }
    }
}
