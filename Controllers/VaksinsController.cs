using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Side;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Data;

namespace ProjectAlpha.Controllers
{
    public class VaksinsController : Controller
    {
        private readonly ProjectAlphaContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public VaksinsController(ProjectAlphaContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }

            var filePath = Path.Combine(MainPath, file.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string extension = Path.GetExtension(filename);
            string constring = string.Empty;
            switch (extension)
            {
                case ".xls":
                    constring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
                case ".xlsx":
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;

            }
            DataTable dt = new DataTable();
            constring = string.Format(constring, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(constring))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        connExcel.Open();

                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }
            constring = "Data Source=SQL5053.site4now.net;Initial Catalog=db_a82205_projectalpha001;User Id=db_a82205_projectalpha001_admin;Password=jbiW5a6Yn6_8evJ;MultipleActiveResultSets=true;";
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.Vaksin";

                    sqlBulkCopy.ColumnMappings.Add("VaksinID", "VaksinID");
                    sqlBulkCopy.ColumnMappings.Add("NamaPegawai", "NamaPegawai");
                    sqlBulkCopy.ColumnMappings.Add("NipPegawai", "NipPegawai");
                    sqlBulkCopy.ColumnMappings.Add("NikPegawai", "NikPegawai");
                    sqlBulkCopy.ColumnMappings.Add("StatusASN", "StatusASN");
                    sqlBulkCopy.ColumnMappings.Add("Vaksin1", "Vaksin1");
                    sqlBulkCopy.ColumnMappings.Add("Vaksin2", "Vaksin2");
                    sqlBulkCopy.ColumnMappings.Add("Vaksin3", "Vaksin3");

                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            ViewBag.Success = "Done";
            return View("Index");
            
        }
        

        // GET: Vaksins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vaksin.ToListAsync());
        }
        public async Task<IActionResult> Gea()
        {
            VaksinViewModel vksn = new VaksinViewModel();

            var vaksin = from a in _context.Vaksin

                        select a;
            var pegawai = from a in _context.Vaksin.Where(a => a.StatusASN == "ASN")
                          select a;
            var pegawai2 = from a in _context.Vaksin.Where(a => a.StatusASN == "Non-ASN")
                           select a;

            var vk1peg = from a in _context.Vaksin.Where(a => a.StatusASN == "ASN" || a.Vaksin3 != "belum")
                         select a;
            var vk1non = from a in _context.Vaksin.Where(a => a.StatusASN == "Non-ASN" || a.Vaksin3 != "belum")
                         select a;
            vksn.TotalPegawaai = pegawai.Count();
            vksn.TotalPegawaai2 = pegawai2.Count();


            vksn.TotalVaksin1 = pegawai.Where(a => a.Vaksin1 != "belum").Count();
            vksn.TotalVaksin1Non = pegawai2.Where(a => a.Vaksin1 != "belum").Count();
            vksn.TotalVaksin2 = pegawai.Where(a => a.Vaksin2 != "belum").Count();
            vksn.TotalVaksin2Non = pegawai2.Where(a => a.Vaksin2 != "belum").Count();
            vksn.TotalVaksin3 = pegawai.Where(a => a.Vaksin3 != "belum").Count();
            vksn.TotalVaksin3Non = pegawai2.Where(a => a.Vaksin3 != "belum").Count();
            return View(vksn);
        }
        
        // GET: Vaksins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaksin = await _context.Vaksin
                .FirstOrDefaultAsync(m => m.VaksinID == id);
            if (vaksin == null)
            {
                return NotFound();
            }

            return View(vaksin);
        }

        // GET: Vaksins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaksins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaksinID,NamaPegawai,NipPegawai,NikPegawai,UnitKerja,StatusASN,Vaksin1,Vaksin2,Vaksin3")] Vaksin vaksin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaksin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaksin);
        }

        // GET: Vaksins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaksin = await _context.Vaksin.FindAsync(id);
            if (vaksin == null)
            {
                return NotFound();
            }
            return View(vaksin);
        }

        // POST: Vaksins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VaksinID,NamaPegawai,NipPegawai,NikPegawai,UnitKerja,StatusASN,Vaksin1,Vaksin2,Vaksin3")] Vaksin vaksin)
        {
            if (id != vaksin.VaksinID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaksin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaksinExists(vaksin.VaksinID))
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
            return View(vaksin);
        }

        // GET: Vaksins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaksin = await _context.Vaksin
                .FirstOrDefaultAsync(m => m.VaksinID == id);
            if (vaksin == null)
            {
                return NotFound();
            }

            return View(vaksin);
        }

        // POST: Vaksins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vaksin = await _context.Vaksin.FindAsync(id);
            _context.Vaksin.Remove(vaksin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaksinExists(string id)
        {
            return _context.Vaksin.Any(e => e.VaksinID == id);
        }

        [HttpGet]
        public async Task<JsonResult> ChartsDB()
        {
            var monitajetty = await _context.Vaksin.Select(j => j.NamaPegawai).Distinct().ToListAsync();
            var jetty = _context.Vaksin
                .Where(r => r.Vaksin1 != "belum")
                .GroupBy(t => t.Vaksin1 != "belum")
                .Select(group => new
                {
                    Vaksin1 = group.Key,
                    Count = group.Count()
                });
            var countJetty = jetty.Select(a => a.Count).ToArray();
            var laut = _context.Vaksin
                .Where(r => r.Vaksin2 != "belum")
                .GroupBy(t => t.Vaksin2 != "belum")
                .Select(group => new
                {
                    Vaksin2 = group.Key,
                    Count = group.Count()
                });
            var countLaut = laut.Select(a => a.Count).ToArray();
            return new JsonResult(new { monita = monitajetty, graphjetty = countJetty, graphlaut = countLaut });


        }
    }
}
