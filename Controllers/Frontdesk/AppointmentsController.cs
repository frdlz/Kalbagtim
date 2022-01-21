using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models;
using WBKNET.Models.Frontdesk;
using WBKNET.Models.Frontdesk.VM;

namespace ProjectAlpha.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ProjectAlphaContext _context;
        private UserManager<AppUser> userManager; //mengambil data user yang terdaftar dari database AppUser
        public AppointmentsController(ProjectAlphaContext context, UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(b => b.StatusFrontdesk != Appointment.status.selesai)
                .ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .OrderBy(a => a.StartDate)
                .Where(b => b.StatusFrontdesk != Appointment.status.selesai)

                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Index3()
        {

            return View();
        }
        public async Task<IActionResult> Index4(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;

            var appointment = from a in _context.Appointment.Include(a => a.LayananFrontdesk)
                              select a;
            if (!String.IsNullOrEmpty(SearchString))
            {
                appointment = appointment.Where(s => s.NomorApp.Equals(SearchString));
            }
            else
            {
                appointment = appointment.Take(0);
            }
            return View(await appointment.AsNoTracking()
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Pendok(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment

                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Dokumen"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Pendok2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .OrderByDescending(a => a.Tanggal)
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Dokumen"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Manifest(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Manifest"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Manifest2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Manifest"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Informasi(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Informasi"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Informasi2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Informasi"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Lainnya(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Lainnya"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }

        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Lainnya2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Lainnya"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Surat(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment

                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Surat"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)

                .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Surat2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment

                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Surat"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)

                .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> PDE(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("PDE"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> PDE2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("PDE"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Analyzing(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Analyzing"))
                .Where(i => i.StatusFrontdesk != Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        [Authorize(Roles = "Frontdesk")]
        public async Task<IActionResult> Analyzing2(string searchstring)
        {
            ViewData["Cfilter"] = searchstring;
            var appoint = from a in _context.Appointment
                          select a;
            if (!String.IsNullOrEmpty(searchstring))
            {
                appoint = appoint.Where(s => s.Tujuan.Contains(searchstring));
            }
            return View(await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .Where(o => o.NamaLayanan.Contains("Analyzing"))
                .Where(i => i.StatusFrontdesk == Appointment.status.selesai)
                 .OrderByDescending(a => a.Tanggal)
                .ToListAsync());
        }
        public ActionResult DetailsView(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewModel mymodel = new ViewModel();



            return View(mymodel);
        }
        public async Task<ActionResult> Count()
        {

            IQueryable<CountViewModel> data =
                from appointment in _context.Appointment.Where(a => a.StatusFrontdesk != Appointment.status.selesai)
                group appointment by appointment.Tanggal.Date into dategroup

                select new CountViewModel()
                {
                    TotalHari = dategroup.Key,
                    DateCount = dategroup.Count(),
                    yourlife = dategroup.Count() + 1

                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var appointment = await _context.Appointment
                .Include(a => a.LayananFrontdesk)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);



            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            PopulateLayananDropdownList();
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentID,Nama,Email,NomorApp,Tanggal,Subject,Deskripsi,Tujuan,NamaLayanan")] Appointment appointment)
        {


            var nomor = DateTime.Now.Ticks.ToString();
            var nomorl = nomor.Substring(nomor.Length - 5);
            var tjn = appointment.NamaLayanan;
            var tjn2 = tjn.Substring(0, 1);

            if (ModelState.IsValid)
            {


                appointment.NomorApp = tjn2 + "-" + nomorl;
                appointment.StartDate = DateTime.Now;
                appointment.StatusFrontdesk = Appointment.status.mulai;
                PopulateLayananDropdownList(ViewBag.NamaLayanan);
                _context.Add(appointment);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = appointment.AppointmentID });
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AppointmentID,NomorApp,Tanggal,Subject,Deskripsi,Tujuan,StartDate,EndDate")] Appointment appointment)
        {
            if (id != appointment.AppointmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentID))
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
            return View(appointment);
        }
        public async Task<IActionResult> Proses(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Appointment
                 .Include(a => a.LayananFrontdesk)

                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Proses")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProsesApp(string id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Appointment
                .Include(a => a.LayananFrontdesk)


                .FirstOrDefaultAsync(m => m.AppointmentID == id);

            if (await TryUpdateModelAsync<Appointment>(pendokToUpdate,
                "",
               a => a.StatusFrontdesk))
            {
                try
                {
                    pendokToUpdate.StatusFrontdesk = Appointment.status.proses;
                    pendokToUpdate.ProcessDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return Redirect(returnUrl);

            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> Selesai(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Appointment
                 .Include(a => a.LayananFrontdesk)

                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Selesai")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelesaiApp(string id, string returnUrl)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Appointment
                .Include(a => a.LayananFrontdesk)


                .FirstOrDefaultAsync(m => m.AppointmentID == id);

            if (await TryUpdateModelAsync<Appointment>(pendokToUpdate,
                "",
               a => a.StatusFrontdesk))
            {
                try
                {
                    pendokToUpdate.StatusFrontdesk = Appointment.status.selesai;
                    pendokToUpdate.EndDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return Redirect(returnUrl);
            }


            return View(pendokToUpdate);
        }
        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool AppointmentExists(string id)
        {
            return _context.Appointment.Any(e => e.AppointmentID == id);
        }
        private void PopulateLayananDropdownList(object selectedPendok = null)
        {
            var pendoksQuery = from d in _context.LayananFrontdesk
                               orderby d.NamaLayanan
                               select d;
            ViewBag.NamaLayanan = new SelectList(pendoksQuery.AsNoTracking(), "NamaLayanan", "NamaLayanan", selectedPendok);
        }
    }
}
