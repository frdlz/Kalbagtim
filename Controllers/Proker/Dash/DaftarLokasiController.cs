using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker;
using ProjectAlpha.Models.Proker.Dash;

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class DaftarLokasiController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public DaftarLokasiController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpGet("AddPegawai/{Id}")]
        public async Task<IActionResult> AddPegawai(string id)
        {
            var muatanlaut = new Penempatan { DaftarLokasiID = id };
            PopulatePegawaiDropdownList();
            return View(muatanlaut);
        }

        [HttpPost("AddPegawai/{Id}")]
        public async Task<IActionResult> CreateAddPegawaig([Bind("DaftarLokasiID,PenempatanID,Mulai,Selesai,ListPegawaiID")] Penempatan muatanlautform)
        {

            if (ModelState.IsValid)
            {
                Penempatan muatanlaut = new Penempatan
                {
                   
                    DaftarLokasiID = muatanlautform.DaftarLokasiID,
                    PenempatanID = muatanlautform.PenempatanID,
                    Mulai = muatanlautform.Mulai,
                    Selesai = muatanlautform.Selesai,
                    ListPegawaiID = muatanlautform.ListPegawaiID,
                   
                   

                };
                PopulatePegawaiDropdownList(ViewBag.ListPegawaiID);
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddPegawai", new Penempatan
                {
                    DaftarLokasiID = muatanlaut.DaftarLokasiID,
                });
            }
            return View(muatanlautform);
        }
        // GET: DaftarLokasi
        public async Task<IActionResult> Index()
        {
            return View(await _context.DaftarLokasi.ToListAsync());
        }
        public async Task<IActionResult> Dashboard()
        {
            return View(await _context.DaftarLokasi.ToListAsync());
        }
        // GET: DaftarLokasi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daftarLokasi = await _context.DaftarLokasi
                .Include(a => a.Penempatans)
                .ThenInclude(a => a.ListPegawai)
                .FirstOrDefaultAsync(m => m.DaftarLokasiID == id);
            if (daftarLokasi == null)
            {
                return NotFound();
            }

            return View(daftarLokasi);
        }

        // GET: DaftarLokasi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DaftarLokasi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DaftarLokasiID,NamaLokasi,Lat,Long,Pengelola,Jenis,Keterangan")] DaftarLokasi daftarLokasi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daftarLokasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(daftarLokasi);
        }

        // GET: DaftarLokasi/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daftarLokasi = await _context.DaftarLokasi.FindAsync(id);
            if (daftarLokasi == null)
            {
                return NotFound();
            }
            return View(daftarLokasi);
        }

        // POST: DaftarLokasi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DaftarLokasiID,NamaLokasi,Lat,Long,Pengelola,Jenis,Keterangan")] DaftarLokasi daftarLokasi)
        {
            if (id != daftarLokasi.DaftarLokasiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daftarLokasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaftarLokasiExists(daftarLokasi.DaftarLokasiID))
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
            return View(daftarLokasi);
        }

        // GET: DaftarLokasi/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daftarLokasi = await _context.DaftarLokasi
                .FirstOrDefaultAsync(m => m.DaftarLokasiID == id);
            if (daftarLokasi == null)
            {
                return NotFound();
            }

            return View(daftarLokasi);
        }

        // POST: DaftarLokasi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var daftarLokasi = await _context.DaftarLokasi.FindAsync(id);
            _context.DaftarLokasi.Remove(daftarLokasi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaftarLokasiExists(string id)
        {
            return _context.DaftarLokasi.Any(e => e.DaftarLokasiID == id);
        }
        private void PopulatePegawaiDropdownList(object selectedPegawai = null)
        {
            var p2sQuery = from d in _context.ListPegawai
                           orderby d.NamaPegawai
                           select d;
            ViewBag.ListPegawaiID = new SelectList(p2sQuery

                .AsNoTracking(), "ListPegawaiID", "NamaPegawai", selectedPegawai);
        }
    }
}
