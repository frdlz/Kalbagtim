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
using ProjectAlpha.Models.Proker.Office.ViewModel;
using System.Linq.Dynamic.Core;

namespace ProjectAlpha.Controllers.Proker.Office
{
    public class BongkarsController : Controller
    {
        private readonly ProjectAlphaContext _context;
        private UserManager<AppUser> userManager;

        public BongkarsController(ProjectAlphaContext context, UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            BongkarViewModel model = new BongkarViewModel();
            
            var bongkar = from a in _context.Bongkar
                          .Where(a => a.Status == StatusBongkar.create || a.Status == StatusBongkar.terima)
                          .Include(a => a.PenggunaJasa)
                          select a;
            var bongkar2 = from a in _context.Bongkar
                          .Where(a => a.Status == StatusBongkar.selesai || a.Status == StatusBongkar.tolak)
                          .Include(a => a.PenggunaJasa)
                          select a;
            var bongkarspabean = from a in _context.Bongkar

                                 .Where(a => a.Pabean == StatusPabean.proses || a.Pabean == StatusPabean.terima) 
                                 .Include(a => a.PenggunaJasa)
                                 select a;
            var bongkarsp2 = from a in _context.Bongkar
                                 .Where(a => a.P2 == StatusP2.proses)
                                 .Include(a => a.PenggunaJasa)
                                 select a;
            var bongkarskk = from a in _context.Bongkar
                                 .Where(a => a.KK == StatusKK.proses)
                                 .Include(a => a.PenggunaJasa)
                             select a;
            var bongkarselesai = from a in _context.Bongkar
                                 .Where(a => a.Status == StatusBongkar.selesai)
                                 .Include(a => a.PenggunaJasa)
                                 select a;
            var barang = from a in _context.PosBarang
                         select a;
            var lokasi = from a in _context.DaftarLokasiPerijinan
                         select a;
            var penlap = from a in _context.PenLap
                         select a;
            var selectBongkar = new BongkarViewModel
            {
                bongkars = await bongkar.Take(5).ToListAsync(),
                bongkars2 = await bongkar2.ToListAsync(),
                daftarLokasiPerijinans = await lokasi.ToListAsync(),
                penLaps = await penlap.ToListAsync(),
                posBarangs = await barang.ToListAsync(),
                bongkarselesai = await bongkarselesai.Take(5).ToListAsync(),
                bongkarspabean = await bongkarspabean.Take(5).ToListAsync(),
                bongkarsp2 = await bongkarsp2.Take(5).ToListAsync(),
                bongkarskakap = await bongkarskk.Take(5).ToListAsync(),

            };
            return View(selectBongkar);
        }
        public async Task<IActionResult> PabeanBongkar()
        {
            
            return View();
        }
        public async Task<IActionResult> P2Bongkar()
        {
            
            return View();
        }
        public async Task<IActionResult> KakapBongkar()
        {
            
            return View();
        }
        public async Task<IActionResult> Pabean()
        {
            BongkarViewModel model = new BongkarViewModel();

            var bongkar = from a in _context.Bongkar
                          .Where(a => a.Status == StatusBongkar.submit)
                          .Include(a => a.PenggunaJasa)
                          select a;
            var barang = from a in _context.PosBarang
                         select a;
            var lokasi = from a in _context.DaftarLokasiPerijinan
                         select a;
            var penlap = from a in _context.PenLap
                         select a;
            var selectBongkar = new BongkarViewModel
            {
                bongkars = await bongkar.ToListAsync(),
                daftarLokasiPerijinans = await lokasi.ToListAsync(),
                penLaps = await penlap.ToListAsync(),
                posBarangs = await barang.ToListAsync()


            };
            return View(selectBongkar);
        }
        public async Task<IActionResult> P2()
        {
            BongkarViewModel model = new BongkarViewModel();

            var bongkar = from a in _context.Bongkar
                          .Where(a => a.P2 == StatusP2.proses)
                          .Include(a => a.PenggunaJasa)
                          select a;
            var barang = from a in _context.PosBarang
                         select a;
            var lokasi = from a in _context.DaftarLokasiPerijinan
                         select a;
            var penlap = from a in _context.PenLap
                         select a;
            var selectBongkar = new BongkarViewModel
            {
                bongkars = await bongkar.ToListAsync(),
                daftarLokasiPerijinans = await lokasi.ToListAsync(),
                penLaps = await penlap.ToListAsync(),
                posBarangs = await barang.ToListAsync()


            };
            return View(selectBongkar);
        }
        public async Task<IActionResult> PenggunaJasaBongkar()
        {
            BongkarViewModel model = new BongkarViewModel();

            var ijinproses = from a in _context.Bongkar
                          .Where(a => a.Status == StatusBongkar.terima || a.Status == StatusBongkar.selesai)
                          .OrderBy(a => a.WkSubmit)
                          .Include(a => a.PenggunaJasa)
                          select a;
            var ijincreate = from a in _context.Bongkar
                          .Where(a => a.Status == StatusBongkar.create || a.Status == StatusBongkar.submit)
                           .OrderBy(a => a.WkSelesai)
                          .Include(a => a.PenggunaJasa)
                           select a;

            var bongkarspabean = from a in _context.Bongkar

                                 .Where(a => a.Pabean == StatusPabean.proses || a.Pabean == StatusPabean.terima)
                                 .Include(a => a.PenggunaJasa)
                                 select a;
            var bongkarsp2 = from a in _context.Bongkar
                                 .Where(a => a.P2 == StatusP2.proses)
                                 .Include(a => a.PenggunaJasa)
                             select a;
            var bongkarskk = from a in _context.Bongkar
                                 .Where(a => a.KK == StatusKK.proses)
                                 .Include(a => a.PenggunaJasa)
                             select a;
            var bongkarselesai = from a in _context.Bongkar
                                 .Where(a => a.Status == StatusBongkar.selesai)
                                 .Include(a => a.PenggunaJasa)
                                 select a;
            var barang = from a in _context.PosBarang
                         select a;
            var lokasi = from a in _context.DaftarLokasiPerijinan
                         select a;
            var penlap = from a in _context.PenLap
                         select a;
            var selectBongkar = new BongkarViewModel
            {
                bongkars = await ijinproses.Take(5).ToListAsync(),
                bongkarproses = await ijincreate.Take(5).ToListAsync(),
                daftarLokasiPerijinans = await lokasi.ToListAsync(),
                penLaps = await penlap.ToListAsync(),
                posBarangs = await barang.ToListAsync(),
                bongkarselesai = await bongkarselesai.Take(5).ToListAsync(),
                bongkarspabean = await bongkarspabean.Take(5).ToListAsync(),
                bongkarsp2 = await bongkarsp2.Take(5).ToListAsync(),
                bongkarskakap = await bongkarskk.Take(5).ToListAsync(),

            };
            return View(selectBongkar);
        }
        public IActionResult IjinProses()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.create || a.Status == StatusBongkar.submit)
                       select new BongkarPenggunaJasaViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinSelesai()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.terima || a.Status == StatusBongkar.selesai)
                              select new BongkarPenggunaJasaViewModel
                              {
                                  IDBongkar = a.BongkarID,
                                  NoPermohonan = a.NoPermohonan,
                                  TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                                  Status = a.Status.ToString(),
                              });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);
            
            
        }
        public IActionResult IjinProsesKakap()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.terima && a.KK == StatusKK.proses)
                       select new BongkarKakapViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusKakap = a.KK.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinSelesaiKakap()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.selesai && a.KK == StatusKK.selesai)
                       select new BongkarKakapViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusKakap = a.KK.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinProsesPabean()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[2][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[2][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.submit)
                       .OrderBy(a => a.TanggalPermohonan)
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusPabean = a.Pabean.ToString(),
                       });
            var assistData = ggd.OrderBy(a => a.TanggalPermohonan).AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinSetujuKasi()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[2][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[2][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Pabean == StatusPabean.kasisetuju)
                       .OrderBy(a => a.TanggalPermohonan)
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusPabean = a.Pabean.ToString(),
                       });
            var assistData = ggd.OrderBy(a => a.TanggalPermohonan).AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinTolakKasi()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[2][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[2][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Pabean == StatusPabean.kasitolak)
                       .OrderBy(a => a.TanggalPermohonan)
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusPabean = a.Pabean.ToString(),
                       });
            var assistData = ggd.OrderBy(a => a.TanggalPermohonan).AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinProsesPabean2()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status != StatusBongkar.selesai || a.Status == StatusBongkar.tolak)
                       join p in _context.ConfirmIjinBongkar on a.BongkarID equals p.BongkarID
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           JenisDok = p.JenisDok,
                           NomorDokumen = p.NomorDokumen,
                           TanggalDok = p.TanggalDok,
                           Status = a.Status.ToString(),
                           StatusPabean = a.Pabean.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinProsesPabean3()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.terima && a.KK == StatusKK.proses)
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusPabean = a.Pabean.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinSelesaiPabean()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.selesai && a.Pabean == StatusPabean.selesai)
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusPabean = a.Pabean.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        public IActionResult IjinProsesP2()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordTotal = 0;
            var ss = (from assist in _context.Bongkar select assist);
            var ggd = (from a in _context.Bongkar.Where(a => a.Status == StatusBongkar.terima && a.P2 == StatusP2.proses)
                       select new BongkarPabeanViewModel
                       {
                           IDBongkar = a.BongkarID,
                           NoPermohonan = a.NoPermohonan,
                           TanggalPermohonan = a.TanggalPermohonan.ToString("d"),
                           Status = a.Status.ToString(),
                           StatusPabean = a.P2.ToString(),
                       });
            var assistData = ggd.AsQueryable();
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NoPermohonan.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        // GET: Bongkars
        public async Task<IActionResult> Index2()
        {
            var asz = _context.Bongkar
                .Include(a => a.PenggunaJasa)
                .Include(b => b.DaftarLokasiPerijinan)
                .Include(a => a.DaftarLokasiPerijinan).ThenInclude(a => a.PenLaps)
                
                .ToListAsync();

            return View(await asz);
        }
        [HttpGet("AddDisposisi/{Id}")]
        public async Task<IActionResult> AddDisposisi(string id)
        {
            var muatanlaut = new Disposisi { BongkarID = id };

            return View(muatanlaut);
        }

        [HttpPost("AddDisposisi/{Id}")]
        public async Task<IActionResult> CreateAddDisposisi([Bind("DisposisiID,DisposisiName,BongkarID")] Disposisi muatanlautform)
        {

            if (ModelState.IsValid)
            {
                Disposisi muatanlaut = new Disposisi
                {
                    DisposisiID = muatanlautform.DisposisiID,
                    DisposisiName = muatanlautform.DisposisiName,
                    
                    BongkarID = muatanlautform.BongkarID,

                };
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddDisposisi", new Disposisi
                {
                    DisposisiID = muatanlaut.DisposisiID,
                });
            }
            return View(muatanlautform);
        }
        [HttpGet("AddND/{Id}")]
        public async Task<IActionResult> AddND(string id)
        {
            var muatanlaut = new NDBongkar { BongkarID = id };

            return View(muatanlaut);
        }

        [HttpPost("AddND/{Id}")]
        public async Task<IActionResult> CreateAddND([Bind("NDBongkarID,NomorND,TanggalND,BongkarID")] NDBongkar muatanlautform)
        {

            if (ModelState.IsValid)
            {
                NDBongkar muatanlaut = new NDBongkar
                {
                    NDBongkarID = muatanlautform.NDBongkarID,
                    NomorND = muatanlautform.NomorND,
                    TanggalND = muatanlautform.TanggalND,
                   

                    BongkarID = muatanlautform.BongkarID,

                };
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return RedirectToAction("KirimP2", new { id = muatanlaut.BongkarID });
            }
            return View(muatanlautform);
        }
        [HttpGet("AddBarang/{Id}")]
        public async Task<IActionResult> AddBarang(string id)
        {
            var muatanlaut = new PosBarang { BongkarID = id };

            return View(muatanlaut);
        }

        [HttpPost("AddBarang/{Id}")]
        public async Task<IActionResult> CreateAddBarang([Bind("PosBarangID,NoPos,BL,Jumlah,JenisKemasan,UraianBarang,BongkarID")] PosBarang muatanlautform)
        {

            if (ModelState.IsValid)
            {
                PosBarang muatanlaut = new PosBarang
                {
                    PosBarangID = muatanlautform.PosBarangID,
                    NoPos = muatanlautform.NoPos,
                    BL = muatanlautform.BL,
                    Jumlah = muatanlautform.Jumlah,
                    JenisKemasan = muatanlautform.JenisKemasan,
                    UraianBarang = muatanlautform.UraianBarang,

                    BongkarID = muatanlautform.BongkarID,

                };
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddBarang", new PosBarang
                {
                    BongkarID = muatanlaut.BongkarID,
                });
            }
            return View(muatanlautform);
        }
        [HttpGet("AddPenlap/{Id}")]
        public async Task<IActionResult> AddPenlap(string id)
        {
            var muatanlaut = new PenLap { BongkarID = id };
            PopulateNomorDropdownList();
            PopulatePetugasP21DropdownList();
            PopulatePetugasP22DropdownList();
            PopulateKasiDropdownList();
            
            return View(muatanlaut);
        }

        [HttpPost("AddPenlap/{Id}")]
        public async Task<IActionResult> CreateAddPenlap([Bind("PenlapID,BongkarID,NoPermohonan,Petugas1,Petugas2,Plh,Kasi,NoBA,TanggalBA,TanggalMulai,DaftarLokasiPerijinanID")] PenLap muatanlautform)
        {

            if (ModelState.IsValid)
            {
                var namanama1 = muatanlautform.Petugas1;
                var namanama2 = muatanlautform.Petugas2;
                var kasi = muatanlautform.Kasi;
                var nipnip1 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama1)).Select(b => b.Nama).Single();
                var nipnip2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama2)).Select(b => b.Nama).Single();
                var nipnip3 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(kasi)).Select(b => b.Nama).Single();
                var pangkat1 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama1)).Select(b => b.Pangkat).Single();
                var pangkat2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama1)).Select(b => b.Pangkat).Single();
                var pangkat3 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(kasi)).Select(b => b.Pangkat).Single();
                var Gol1 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama2)).Select(b => b.Golongan).Single();
                var Gol2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama2)).Select(b => b.Golongan).Single();
                var Gol3 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(kasi)).Select(b => b.Golongan).Single();
                var Jab1 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama2)).Select(b => b.Jabatan).Single();
                var Jab2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(namanama2)).Select(b => b.Jabatan).Single();
                var Jab3 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(kasi)).Select(b => b.Jabatan).Single();
                PenLap muatanlaut = new PenLap
                {
                    PenlapID = muatanlautform.PenlapID,
                    NoPermohonan = muatanlautform.NoPermohonan,
                    Petugas1 = muatanlautform.Petugas1,
                    Petugas2 = muatanlautform.Petugas2,
                    NamaPetugas1 = nipnip1,
                    NamaPetugas2 = nipnip2,
                    Pangkat1 = pangkat1,
                    Pangkat2 = pangkat2,
                    Golongan1 = Gol1,
                    Golongan2 = Gol2,
                    Jabatan1 = Jab1,
                    Jabatan2 = Jab2,
                    Plh = muatanlautform.Plh,
                    Kasi = muatanlautform.Kasi,
                    NoBA = muatanlautform.NoBA,
                    TanggalBA = muatanlautform.TanggalBA,
                    TanggalMulai = muatanlautform.TanggalMulai,
                    
                    BongkarID = muatanlautform.BongkarID,

                };
                PopulateNomorDropdownList(ViewBag.NoPermohonan);
                PopulatePetugasP21DropdownList(ViewBag.Petugas1);
                PopulatePetugasP22DropdownList(ViewBag.Petugas2);
                PopulateKasiDropdownList(ViewBag.Kasi);
                
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddPenlap", new PenLap
                {
                    BongkarID = muatanlaut.BongkarID,
                });
            }
            return View(muatanlautform);
        }
        [HttpGet("AddLapTimbun/{Id}")]
        public async Task<IActionResult> AddLapTimbun(string id)
        {
            var muatanlaut = new LapTimbun { BongkarID = id };
           

            return View(muatanlaut);
        }

        [HttpPost("AddLapTimbun/{Id}")]
        public async Task<IActionResult> CreateAddLapTimbun([Bind("LapTimbunID,NomorLap,Tanggal,BongkarID,Keterangan,NamaPejabat")] LapTimbun muatanlautform)
        {

            if (ModelState.IsValid)
            {
                LapTimbun muatanlaut = new LapTimbun
                {
                    LapTimbunID = muatanlautform.LapTimbunID,
                    NomorLap = muatanlautform.NomorLap,
                    Tanggal = muatanlautform.Tanggal,
                    
                    Keterangan = muatanlautform.Keterangan,
                    NamaPejabat = muatanlautform.NamaPejabat,
                    

                    BongkarID = muatanlautform.BongkarID,

                };
               

                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddLapTimbun", new LapTimbun
                {
                    BongkarID = muatanlaut.BongkarID,
                });
            }
            return View(muatanlautform);
        }
        [HttpGet("AddConfirm/{Id}")]
        public async Task<IActionResult> AddConfirm(string id)
        {
            var muatanlaut = new ConfirmIjinBongkar { BongkarID = id };


            return View(muatanlaut);
        }

        [HttpPost("AddConfirm/{Id}")]
        public async Task<IActionResult> CreateAddConfirm([Bind("ConfirmIjinBongkarID,JenisDok,NomorDokumen,TanggalDok,StatusDok,BongkarID")] ConfirmIjinBongkar muatanlautform)
        {

            if (ModelState.IsValid)
            {
                ConfirmIjinBongkar muatanlaut = new ConfirmIjinBongkar
                {
                    ConfirmIjinBongkarID = muatanlautform.ConfirmIjinBongkarID,
                    JenisDok = muatanlautform.JenisDok,
                    NomorDokumen = muatanlautform.NomorDokumen,
                    TanggalDok = muatanlautform.TanggalDok,
                    StatusDok = muatanlautform.StatusDok,
                    BongkarID = muatanlautform.BongkarID,

                };
                _context.Add(muatanlaut);
                await _context.SaveChangesAsync();

                return View("AddConfirm", new ConfirmIjinBongkar
                {
                    BongkarID = muatanlaut.BongkarID,
                });
            }
            return View(muatanlautform);
        }
        
        // GET: Bongkars/Details/5
        public async Task<IActionResult> PrintPersetujuan(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .Include(a => a.PenggunaJasa)
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (bongkar == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(bongkar)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
               ;
           
        }

        public async Task<IActionResult> PrintPengantarPersetujuan(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            PrintViewModel model = new PrintViewModel();
            var bongkarw = from a in _context.Bongkar
                         
                          select a;
            
            var bongkar = new PrintViewModel
            {
                Bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .Include(a => a.PegawaiList)
                .Include(a => a.PenggunaJasa)
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                PenLap = await _context.PenLap.FirstOrDefaultAsync(m => m.BongkarID == id),
                ConfirmIjinBongkar = await _context.ConfirmIjinBongkar.FirstOrDefaultAsync(m => m.BongkarID == id),
                


        };
            if (bongkar == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(bongkar)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
               ;

        }
        public async Task<IActionResult> PrintLapBongkar(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PrintViewModel model = new PrintViewModel();

            var bongkar = new PrintViewModel
            {
                Bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)

                .Include(a => a.PenggunaJasa)
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                PenLap = await _context.PenLap.FirstOrDefaultAsync(m => m.BongkarID == id),
                ConfirmIjinBongkar = await _context.ConfirmIjinBongkar.FirstOrDefaultAsync(m => m.BongkarID == id),

            };
            if (bongkar == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(bongkar)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
               ;

        }
        public async Task<IActionResult> PrintBA(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PrintBAViewModel model = new PrintBAViewModel();
            
            
           
            var bongkar = new PrintBAViewModel
            {
                
                Bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)

                .Include(a => a.PenggunaJasa)
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                PenLap = await _context.PenLap
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                ConfirmIjinBongkar = await _context.ConfirmIjinBongkar.FirstOrDefaultAsync(m => m.BongkarID == id),
                
            };

        

            if (bongkar == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(bongkar)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
               ;

        }
        public async Task<IActionResult> PrintNDP2(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PrintBAViewModel model = new PrintBAViewModel();



            var bongkar = new PrintBAViewModel
            {

                Bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)

                .Include(a => a.PenggunaJasa)
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                PenLap = await _context.PenLap
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                ConfirmIjinBongkar = await _context.ConfirmIjinBongkar.FirstOrDefaultAsync(m => m.BongkarID == id),
                NDBongkar = await _context.NDBongkar.FirstOrDefaultAsync(a => a.BongkarID == id)
            };



            if (bongkar == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(bongkar)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
               ;

        }
        public async Task<IActionResult> PrintSTP2(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PrintBAViewModel model = new PrintBAViewModel();



            var bongkar = new PrintBAViewModel
            {

                Bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)

                .Include(a => a.PenggunaJasa)
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                PenLap = await _context.PenLap
                .Include(a => a.DaftarLokasiPerijinan)
                .FirstOrDefaultAsync(m => m.BongkarID == id),
                ConfirmIjinBongkar = await _context.ConfirmIjinBongkar.FirstOrDefaultAsync(m => m.BongkarID == id),

            };



            if (bongkar == null)
            {
                return NotFound();
            }
            return new Rotativa.AspNetCore.ViewAsPdf(bongkar)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = { Left = 20, Bottom = 20, Right = 10, Top = 10 },
            }
               ;

        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bongkar = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (bongkar == null)
            {
                return NotFound();
            }

            return View(bongkar);
        }
        public async Task<IActionResult> LapTimbun(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bongkar = await _context.Bongkar
                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (bongkar == null)
            {
                return NotFound();
            }

            return View(bongkar);
        }
        // GET: Bongkars/Create
        public IActionResult Create()
        {
            PopulateLokasiDropdownList();
            return View();
        }

        // POST: Bongkars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BongkarID,PenggunaJasaID,DaftarLokasiPerijinanID,NoPermohonan,TanggalPermohonan,NomorBC11,TanggalBC11,NamaSarkut,NoBL,TanggalBL,JumlahBrg,JenisBrg,Bruto,Volume,Alasan,Hal")] Bongkar bongkar)
        {
            if (ModelState.IsValid)
            {
                
                bongkar.PenggunaJasaID = userManager.GetUserId(User);
                bongkar.WkCreate = DateTime.Now;
                PopulateLokasiDropdownList(ViewBag.DaftarLokasiPerijinanID);
                _context.Add(bongkar);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddBarang", new { id = bongkar.BongkarID });
            }
            return View(bongkar);
        }

        // GET: Bongkars/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bongkar = await _context.Bongkar.FindAsync(id);
            if (bongkar == null)
            {
                return NotFound();
            }
            return View(bongkar);
        }

        // POST: Bongkars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BongkarID,NomorBC11,TanggalBC11,NamaSarkut,NoBL,TanggalBL,JumlahBrg,JenisBrg,Bruto,Volume,Lokasi,Alasan")] Bongkar bongkar)
        {
            if (id != bongkar.BongkarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bongkar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BongkarExists(bongkar.BongkarID))
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
            return View(bongkar);
        }
        public async Task<IActionResult> KirimIjin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("KirimIjin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IjinKirim(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.Pabean, a => a.WkSubmit))
            {
                try
                {
                    pendokToUpdate.WkSubmit = DateTime.Now;
                    pendokToUpdate.Status = StatusBongkar.submit;
                    pendokToUpdate.Pabean = StatusPabean.proses;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(Index));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> Disposisi(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Disposisi")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisposisiKK(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.Pabean, a => a.WkSubmit))
            {
                try
                {
                    pendokToUpdate.WkSubmit = DateTime.Now;
                    pendokToUpdate.Status = StatusBongkar.submit;
                    pendokToUpdate.Pabean = StatusPabean.proses;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(Index));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> SelesaiP2(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .Include(a => a.PegawaiList)

                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("SelesaiP2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IjinSelesaiP2(string? id)

        {
            if (id == null)
            {
                return NotFound();
            }
           
           
            
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .Include(a => a.PegawaiList)

                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.P2))
            {
                try
                {

                    
                    pendokToUpdate.Status = StatusBongkar.terima;
                    
                    pendokToUpdate.P2 = StatusP2.selesai;
                    
                   

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(KakapBongkar));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> SelesaiIjin(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .Include(a => a.PegawaiList)

                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("SelesaiIjin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IjinSelesai(string? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            var usid = userManager.GetUserId(User);
            var p2sQuery = from d in _context.PegawaiList.Where(a => a.PegawaiListID == usid)
                           
                           select d;
            var nn2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(usid)).Select(b => b.Nama).Single();
            var nama = p2sQuery.Select(a => a.Nama).Single();
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)
                .Include(a => a.PegawaiList)

                .FirstOrDefaultAsync(m => m.BongkarID == id);
            
            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.Pabean, a => a.P2, a => a.KK, a => a.WkSelesai))
            {
                
                try
                {
                    
                    pendokToUpdate.WkSelesai = DateTime.Now;
                    pendokToUpdate.Status = StatusBongkar.selesai;
                    pendokToUpdate.Pabean = StatusPabean.selesai;
                    pendokToUpdate.P2 = StatusP2.selesai;
                    pendokToUpdate.KK = StatusKK.selesai;
                    pendokToUpdate.Kakap = userManager.GetUserId(User);
                    pendokToUpdate.NamaKakap = nn2;
                    pendokToUpdate.JabatanKakap = p2sQuery.Select(a => a.Jabatan).Single().ToString();
                    pendokToUpdate.WkKakap = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(KakapBongkar));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> PabeanTerima(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("PabeanTerima")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pater(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.Pabean, a => a.WkPetugasTerimaPabean, a => a.PetugasTerimaPabean))
            {
                try
                {

                    pendokToUpdate.Status = StatusBongkar.terima;
                    pendokToUpdate.Pabean = StatusPabean.terima;
                    pendokToUpdate.WkPetugasTerimaPabean = DateTime.Now;
                    pendokToUpdate.PetugasTerimaPabean = userManager.GetUserName(User);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction("AddConfirm", new { id = pendokToUpdate.BongkarID });
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> SetujuKasiPabean(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("SetujuKasiPabean")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KasiPabeanSetuju(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usid = userManager.GetUserId(User);
            var p2sQuery = from d in _context.PegawaiList.Where(a => a.PegawaiListID == usid)

                           select d;
            var nn2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(usid)).Select(b => b.Nama).Single();
            var nama = p2sQuery.Select(a => a.Nama).Single();
            var jabatan = p2sQuery.Select(a => a.Jabatan).Single();
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.Pabean, a => a.WkPetugasTerimaPabean, a => a.PetugasTerimaPabean))
            {
                try
                {

                    pendokToUpdate.Pabean = StatusPabean.kasisetuju;
                    pendokToUpdate.WkKasiPabean = DateTime.Now;
                    pendokToUpdate.KasiPabean = userManager.GetUserId(User);
                    pendokToUpdate.NamaKasiPabean = nama.ToString();
                    pendokToUpdate.JabatanKasiPabean = jabatan.ToString();

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(PabeanBongkar));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> TolakKasiPabean(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("TolakKasiPabean")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KasiPabeanTolak(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usid = userManager.GetUserId(User);
            var p2sQuery = from d in _context.PegawaiList.Where(a => a.PegawaiListID == usid)

                           select d;
            var nn2 = _context.PegawaiList.Where(a => a.PegawaiListID.Contains(usid)).Select(b => b.Nama).Single();
            var nama = p2sQuery.Select(a => a.Nama).Single();
            var jabatan = p2sQuery.Select(a => a.Jabatan).Single();
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status, a => a.Pabean, a => a.WkPetugasTerimaPabean, a => a.PetugasTerimaPabean))
            {
                try
                {

                    pendokToUpdate.Pabean = StatusPabean.kasitolak;
                    pendokToUpdate.WkKasiPabean = DateTime.Now;
                    pendokToUpdate.KasiPabean = userManager.GetUserId(User);
                    pendokToUpdate.NamaKasiPabean = nama.ToString();
                    pendokToUpdate.JabatanKasiPabean = jabatan.ToString();

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(PabeanBongkar));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> KirimKK(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("KirimKK")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kirim2K(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Status,a => a.Pabean, a => a.KK))
            {
                try
                {
                    pendokToUpdate.Status = StatusBongkar.terima;
                    pendokToUpdate.Pabean = StatusPabean.kirimkk;
                    pendokToUpdate.KK = StatusKK.proses;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(Index));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> KirimP2(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)
                

                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("KirimP2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kirim2P(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)
                

                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Pabean, a => a.P2))
            {
                try
                {
                    pendokToUpdate.Pabean = StatusPabean.kirimp2;
                    pendokToUpdate.P2 = StatusP2.proses;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(Index));
            }


            return View(pendokToUpdate);
        }
        public async Task<IActionResult> KirimPabean(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendok = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (pendok == null)
            {
                return NotFound();
            }
            return View(pendok);
        }

        // POST: Pendoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("KirimPabean")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PabeanKirim(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendokToUpdate = await _context.Bongkar
                .Include(a => a.PosBarangs)


                .FirstOrDefaultAsync(m => m.BongkarID == id);

            if (await TryUpdateModelAsync<Bongkar>(pendokToUpdate,
                "",
               a => a.Pabean, a => a.P2))
            {
                try
                {
                    pendokToUpdate.Pabean = StatusPabean.selesaip2;
                    pendokToUpdate.P2 = StatusP2.selesai;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
                return RedirectToAction(nameof(Index));
            }


            return View(pendokToUpdate);
        }
        
        // GET: Bongkars/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bongkar = await _context.Bongkar
                .FirstOrDefaultAsync(m => m.BongkarID == id);
            if (bongkar == null)
            {
                return NotFound();
            }

            return View(bongkar);
        }

        // POST: Bongkars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bongkar = await _context.Bongkar.FindAsync(id);
            _context.Bongkar.Remove(bongkar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BongkarExists(string id)
        {
            return _context.Bongkar.Any(e => e.BongkarID == id);
        }
        private void PopulateLokasiDropdownList(object selectedPendok = null)
        {
            var pendoksQuery = from d in _context.DaftarLokasiPerijinan
                               orderby d.NamaLokasi
                               select d;
            ViewBag.DaftarLokasiPerijinanID = new SelectList(pendoksQuery.AsNoTracking(), "DaftarLokasiPerijinanID", "NamaLokasi", selectedPendok);
        }
        private void PopulateNomorDropdownList(object selectedPendok = null)
        {
            var pendoksQuery = from d in _context.Bongkar
                               orderby d.NoPermohonan
                               select d;
            ViewBag.NoPermohonan = new SelectList(pendoksQuery.AsNoTracking(), "NoPermohonan", "NoPermohonan", selectedPendok);
        }
        private void PopulatePetugasP21DropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PegawaiList.Where(a => a.Penempatan == "P2")
                           orderby d.Nama
                           select d;
            ViewBag.Petugas1 = new SelectList(p2sQuery

                .AsNoTracking(), "PegawaiListID", "Nama", selectedPetugasP2);
        }
        private void PopulatePetugasP22DropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PegawaiList.Where(a => a.Penempatan == "P2")
                           orderby d.Nama
                           select d;
            ViewBag.Petugas2 = new SelectList(p2sQuery

                .AsNoTracking(), "PegawaiListID", "Nama", selectedPetugasP2);
        }
        private void PopulateKasiDropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PegawaiList.Where(a => a.Penempatan == "P2")
                           orderby d.Nama
                           select d;
            ViewBag.Kasi = new SelectList(p2sQuery

                .AsNoTracking(), "PegawaiListID", "Nama", selectedPetugasP2);
        }
    }
}
