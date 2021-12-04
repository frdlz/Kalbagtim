using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models;
using ProjectAlpha.Models.P2KP.ViewModel;
using ProjectAlpha.Models.ViewModel;

namespace ProjectAlpha.Controllers
{
    public class P2kpController : Controller
    {
        private readonly ProjectAlphaContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public P2kpController(ProjectAlphaContext context, IHostingEnvironment environment)
        {
            _context = context;
            hostingEnvironment = environment;
        }
        public async Task<IActionResult> IndexDatatables()
        {
            var p2kp = _context.P2kp
                .Include(a => a.Narsum)
                .AsNoTracking();
            return View(await p2kp.ToListAsync());
        }
        // GET: P2kp
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["MateriSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var p2kp = _context.P2kp
                .Include(a => a.Narsum)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                p2kp = p2kp.Where(a => a.Judul.Contains(searchString) || a.Narsum.Narasumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    p2kp = p2kp.OrderByDescending(s => s.Judul);
                    break;
                case "Date":
                    p2kp = p2kp.OrderBy(a => a.Tanggal);
                    break;
                case "date_desc":
                    p2kp = p2kp.OrderByDescending(a => a.Tanggal);
                    break;
                default:
                    p2kp = p2kp.OrderBy(a => a.Tanggal);
                    break;

            }
            int pageSize = 3;

            return View(await PaginatedList<P2kp>.CreateAsync(p2kp.AsNoTracking(), pageNumber ?? 1, pageSize));
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

       
        [HttpPost, ActionName("Selesai")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WaktuSelesai(int id, [Bind("P2kpID,Judul,Tanggal,JamMulai,JamSelesai,Tempat,NarsumID,Status,WaktuSelesai")] P2kp p2kp)
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
        private void PopulateJenisFileDropdownList(object selectedFile = null)
        {
            var filesQuery = from d in _context.JenisFile
                               orderby d.JenisFileID
                               select d;
            ViewBag.JenisFileID = new SelectList(filesQuery.AsNoTracking(), "JenisFileID", "FileType", selectedFile);
        }
        [HttpGet("P2kp/Image/{Id}")]
        public async Task<IActionResult> Image(int? id)
        {
            var imagep2kp = new ImageP2kpFormViewModel { P2kpID = id.Value };

            return View(imagep2kp);
        }

        [HttpPost("P2kp/Image/{Id}")]
        public async Task<IActionResult> CreateImageP2kp([Bind("IDImageP2kp,ImageName,UploadDate,Image,P2kpID")] ImageP2kpFormViewModel imagep2kpform)
        {

            var xcv = string.Format(@"{0}", Guid.NewGuid());
            if (ModelState.IsValid)
            {
                ImageP2kp imagep2kp = new ImageP2kp
                {
                    ImageP2kpID = imagep2kpform.IDImageP2kp,
                    P2kpID = imagep2kpform.P2kpID,
                    UploadDate = DateTime.Now,

                    ImageName = xcv
                };



                var formFile = imagep2kpform.Image;
                if (formFile == null || formFile.Length == 0)
                {
                    ModelState.AddModelError("", "Uploaded file is empty or null.");
                    return View(viewName: "Index");
                }
                var uploadsRootFolder = Path.Combine(hostingEnvironment.WebRootPath, "ImageP2kp");
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                var filePath = Path.Combine(uploadsRootFolder, xcv + "." + "jpg");
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }

                _context.Add(imagep2kp);
                await _context.SaveChangesAsync();

                return View("Image", new ImageP2kpFormViewModel
                {
                    P2kpID = imagep2kp.P2kpID,
                });
            }

            return View(imagep2kpform);
        }
        [HttpGet("P2kp/Materi/{Id}")]
        public async Task<IActionResult> Materi(int? id)
        {
            var materip2kp = new MateriP2kpFormViewModel { P2kpID = id.Value };
           
            return View(materip2kp);
        }

        [HttpPost("P2kp/Materi/{Id}")]
        public async Task<IActionResult> CreateMateriMonita([Bind("IDMateriP2kp,ImageName,UploadDate,File,FileType,P2kpID")] MateriP2kpFormViewModel materip2kpform)
        {

            var xcv = string.Format(@"{0}", Guid.NewGuid());
            if (ModelState.IsValid)
            {
                MateriP2kp materip2kp = new MateriP2kp
                {
                    MateriP2kpID = materip2kpform.IDMateriP2kp,
                    P2kpID = materip2kpform.P2kpID,
                    UploadDate = DateTime.Now,
                    JenisFIleID = materip2kpform.JenisFIleID,
                    MateriName = xcv
                };


                var namafile = materip2kp.jenisFile.FileType;
                var formFile = materip2kpform.File;
                if (formFile == null || formFile.Length == 0)
                {
                    ModelState.AddModelError("", "Uploaded file is empty or null.");
                    return View(viewName: "Index");
                }
                var uploadsRootFolder = Path.Combine(hostingEnvironment.WebRootPath, "MateriP2kp");
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                var filePath = Path.Combine(uploadsRootFolder, xcv + "." + namafile);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }

                _context.Add(materip2kp);
                await _context.SaveChangesAsync();

                return View("Materi", new MateriP2kpFormViewModel
                {
                    P2kpID = materip2kp.P2kpID,
                });
            }
            PopulateJenisFileDropdownList(materip2kpform.JenisFIleID);
            return View(materip2kpform);
        }
    }
}
