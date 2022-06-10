using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Dash;
using ProjectAlpha.Models.Proker.Dash.ViewModel;

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class DashboardsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public DashboardsController(ProjectAlphaContext context)
        {
            _context = context;
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dashboard.ToListAsync());
        }
        public async Task <IActionResult> Impor(string selectYear)
        {
            IQueryable<string> query = from a in _context.LapImpor
                                       orderby a.Year
                                       select a.Year;
            

            LapImporViewModel model = new LapImporViewModel();
            var target = from a in _context.Target
                         select a;
            
            var impor = from a in _context.LapImpor

                        select a;
           
            var topImpor = from a in _context.TopImporBM
                           
                           select a;

            var topLokasi = from a in _context.TopLokasi

                           select a;
            var topNegaraAsals = from a in _context.TopNegaraAsal

                            select a;
            var topKomoditi = from a in _context.TopKomoditi

                                 select a;
            var topImportirDev = from a in _context.TopImportirDev

                              select a;
            var daftarLok = from a in _context.DaftarLokasi

                                 select a;
            var thisyear = DateTime.Now.Year.ToString();
            var sss = _context.LapImpor.Where(a => a.Year == thisyear).Sum(a => a.BM);
            var sss2 = _context.Target.Where(a => a.TahunTarget == thisyear).Sum(a => a.BM);


            var selectYearM = new LapImporViewModel
            {

                Targets = await target.ToListAsync(),
                Year = new SelectList(await query.Distinct().ToListAsync()),
                lapImpors = await impor.ToListAsync(),
                TopImporBMs = await topImpor.ToListAsync(),
                TopLokasis = await topLokasi.ToListAsync(),
                TopNegaraAsal = await topNegaraAsals.ToListAsync(),
                TopKomoditis = await topKomoditi.ToListAsync(),
                TopImportirDevs = await topImportirDev.ToListAsync(),
                DaftarLokasis = await daftarLok.ToListAsync(),
                TotalBM = _context.LapImpor.Where(a => a.Year == thisyear).Sum(a => a.BM),
                TargetBMYear = _context.Target.Where(a => a.TahunTarget == thisyear).Sum(a => a.BM),
                TotalPIB = _context.LapImpor.Where(a => a.Year == thisyear).Sum(a => a.JumlahPIB),
                TotalTon = _context.LapImpor.Where(a => a.Year == thisyear).Sum(a => a.Tonase),
                TotalDevisa = _context.LapImpor.Where(a => a.Year == thisyear).Sum(a => a.Devisa),
                PercentBMYear = ((sss/sss2)*100),
            };
            
            return View(selectYearM);
        }
        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.DashboardID == id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DashboardID,DashboardName")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dashboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dashboard);
        }

        // GET: Dashboards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == "b7641af3-dc5d-439f-808a-3d2a4afc5967")
            {
                
            }

            var dashboard = await _context.Dashboard.FindAsync(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            return View(dashboard);
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DashboardID,DashboardName")] Dashboard dashboard)
        {
            if (id != dashboard.DashboardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dashboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashboardExists(dashboard.DashboardID))
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
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.DashboardID == id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dashboard = await _context.Dashboard.FindAsync(id);
            _context.Dashboard.Remove(dashboard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DashboardExists(string id)
        {
            return _context.Dashboard.Any(e => e.DashboardID == id);
        }
    }
}
