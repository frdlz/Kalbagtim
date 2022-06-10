using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Dash;
using System.Data.OleDb;
using System.Data.SqlClient;
using Google.DataTable.Net.Wrapper.Extension;
using Google.DataTable.Net.Wrapper;

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class LapImporsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public LapImporsController(ProjectAlphaContext context)
        {
            _context = context;
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
            System.Data.DataTable dt = new System.Data.DataTable();
            constring = string.Format(constring, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(constring))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        connExcel.Open();

                        System.Data.DataTable dtExcelSchema;
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
            
            constring = "Data Source=SQL5045.site4now.net; Initial Catalog = DB_A4EE98_newdataoasys; User Id = DB_A4EE98_newdataoasys_admin; Password = Balikpapan1000;MultipleActiveResultSets=true;";
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.LapImpor";

                    sqlBulkCopy.ColumnMappings.Add("LapImporID", "LapImporID");
                    sqlBulkCopy.ColumnMappings.Add("Bulan", "Bulan");
                    sqlBulkCopy.ColumnMappings.Add("BM", "BM");
                    sqlBulkCopy.ColumnMappings.Add("JumlahPIB", "JumlahPIB");
                    sqlBulkCopy.ColumnMappings.Add("Tonase", "Tonase");
                    sqlBulkCopy.ColumnMappings.Add("Devisa", "Devisa");
                    sqlBulkCopy.ColumnMappings.Add("Year", "Year");
                    
                    

                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            ViewBag.Success = "Done";
            return View("Index");

        }

        // GET: LapImpors
        public async Task<IActionResult> Index()
        {
            return View(await _context.LapImpor
                
                .ToListAsync());
        }
       

        // GET: LapImpors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapImpor = await _context.LapImpor
                .FirstOrDefaultAsync(m => m.LapImporID == id);
            if (lapImpor == null)
            {
                return NotFound();
            }

            return View(lapImpor);
        }

        // GET: LapImpors/Create
        public IActionResult Create()
        {
            PopulatelapDropdownList();
            return View();
        }

        // POST: LapImpors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LapImporID,Bulan,Year,BM,JumlahPIB,Tonase,Devisa")] LapImpor lapImpor)
        {
            
            if (ModelState.IsValid)
            {
                LapImpor lap = new LapImpor
                {
                    LapImporID = lapImpor.LapImporID,
                    Bulan = lapImpor.Bulan,
                    
                    BM = lapImpor.BM,
                    JumlahPIB = lapImpor.JumlahPIB,
                    Tonase = lapImpor.Tonase,
                    Devisa = lapImpor.Devisa,
                };
                PopulatelapDropdownList(ViewBag.Year);
                _context.Add(lapImpor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lapImpor);
        }
        public async Task<JsonResult> ChartsMuatMonthly()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.LapImpor
                .Select(a => a.Bulan.Month)
                .Distinct().ToListAsync();
            var year = await _context.LapImpor
                .Select(a => a.Year)
                .Distinct().ToListAsync();
            var month = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
            var month2 = _context.LapImpor
                
                .Where(a => a.Year == "2022")
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth2 = month2.Select(a => a.Sum).ToArray();
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth, tbm2 = countmonth2 });
        }
        public async Task<JsonResult> ChartsTonnaseMonthly()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.LapImpor
                .Select(a => a.Bulan.Month)
                .Distinct().ToListAsync();
            var year = await _context.LapImpor
                .Select(a => a.Year)
                .Distinct().ToListAsync();
            var month = _context.LapImpor
                
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Tonase)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
            var month2 = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Devisa)
                });
            var countmonth2 = month2.Select(a => a.Sum).ToArray();
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth, tbm2 = countmonth2 });
        }
        public async Task<JsonResult> ChartsKomoditi()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.LapImpor
                .Select(a => a.Bulan.Month)
                .Distinct().ToListAsync();
            var year = await _context.LapImpor
                .Select(a => a.Year)
                .Distinct().ToListAsync();
            var month = _context.LapImpor
                
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Tonase)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
            var month2 = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Devisa)
                });
            var countmonth2 = month2.Select(a => a.Sum).ToArray();
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth, tbm2 = countmonth2 });
        }
        public async Task<JsonResult> TopimportirBM()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.TopImporBM
                .OrderBy(a => a.BM)
                .Select(a => a.NamaImortir.Substring(0,16))
                
                .Distinct().ToListAsync();
            
            var month = _context.TopImporBM
                
                .Where(a => a.Year == thisyear)
                .OrderBy(a => a.BM) 
                .GroupBy(a => a.NamaImortir)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
           
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth });
        }

        public async Task<JsonResult> TopLokasiBM()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.TopLokasi
                .Select(a => a.Nama.Substring(0, 16))
                .Distinct().ToListAsync();

            var month = _context.TopLokasi
                .Include(a => a.LapYear)
                .Where(a => a.LapYear.Year == thisyear)
                .GroupBy(a => a.Nama)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();

            return new JsonResult(new { lapbulan = bulan, tbm = countmonth });
        }
        public async Task<JsonResult> TopKomoditiDevisa()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.TopKomoditi
                .Select(a => a.Nama.Substring(0, 16))
                .Distinct().ToListAsync();

            var month = _context.TopKomoditi
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Nama)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Devisa)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();

            return new JsonResult(new { lapbulan = bulan, tbm = countmonth });
        }
        public async Task<JsonResult> TopKomoditiTonnase()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.TopKomoditi
                .Select(a => a.Nama.Substring(0, 16))
                .Distinct().ToListAsync();

            var month = _context.TopKomoditi
                
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Nama)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Tonnase)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();

            return new JsonResult(new { lapbulan = bulan, tbm = countmonth });
        }
        public async Task<JsonResult> TopNegaraDevisa()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.TopNegaraAsal
                .Select(a => a.NamaNegara.Substring(0, 16))
                .Distinct().ToListAsync();

            var month = _context.TopNegaraAsal
                
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.NamaNegara)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Devisa)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();

            return new JsonResult(new { Country = bulan, Popularity = countmonth });
        }
        public async Task<JsonResult> TopImportirDevisa()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.TopImportirDev
                .Select(a => a.Nama.Substring(0, 22))
                .Distinct().ToListAsync();

            var month = _context.TopImportirDev
                
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Nama)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Devisa)
                })
                ;
            var countmonth = month.Select(a => a.Sum).ToArray();

            return new JsonResult(new { lapbulan = bulan, tbm = countmonth });
        }
        public async Task<JsonResult> ChartsMuatMonthly2()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.LapImpor
                .Select(a => a.Bulan.Month)
                .Distinct().ToListAsync();
            var year = await _context.LapImpor
                .Select(a => a.Year)
                .Distinct().ToListAsync();
            var month = _context.LapImpor
              
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.JumlahPIB)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
            var month2 = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth2 = month2.Select(a => a.Sum).ToArray();
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth, tbm2 = countmonth2 });
        }
        public async Task<JsonResult> ChartsMuatMonthly3()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.LapImpor
                .Select(a => a.Bulan.Month)
                .Distinct().ToListAsync();
            var year = await _context.LapImpor
                .Select(a => a.Year)
                .Distinct().ToListAsync();
            var month = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Tonase)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
            var month2 = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth2 = month2.Select(a => a.Sum).ToArray();
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth, tbm2 = countmonth2 });
        }
        public async Task<JsonResult> ChartsMuatMonthly4()
        {
            var thisyear = DateTime.Now.Year.ToString();
            var bulan = await _context.LapImpor
                .Select(a => a.Bulan.Month)
                .Distinct().ToListAsync();
            var year = await _context.LapImpor
                .Select(a => a.Year)
                .Distinct().ToListAsync();
            var month = _context.LapImpor
               
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.Devisa)
                });
            var countmonth = month.Select(a => a.Sum).ToArray();
            var month2 = _context.LapImpor
                
                .Where(a => a.Year == thisyear)
                .GroupBy(a => a.Bulan)
                .Select(group => new
                {
                    BM = group.Key,
                    Sum = group.Sum(a => a.BM)
                });
            var countmonth2 = month2.Select(a => a.Sum).ToArray();
            return new JsonResult(new { lapbulan = bulan, tbm = countmonth, tbm2 = countmonth2 });
        }
        public ActionResult GeoDevisa()
        {
            var negaradevisa = _context.TopNegaraAsal;
            var json = negaradevisa.ToGoogleDataTable()
                .NewColumn(new Column(ColumnType.String, "Negara"), x => x.NamaNegara)
                .NewColumn(new Column(ColumnType.Number, "Devisa"), x => x.Devisa)
                .NewColumn(new Column(ColumnType.Number, "Tonasse"), x => x.Tonnase)
                .Build()
                .GetJson()
                ;
            return Content(json);
        }
        // GET: LapImpors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapImpor = await _context.LapImpor.FindAsync(id);
            if (lapImpor == null)
            {
                return NotFound();
            }
            return View(lapImpor);
        }

        // POST: LapImpors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LapImporID,Bulan,Tahun,BM,JumlahPIB,Tonase,Devisa")] LapImpor lapImpor)
        {
            if (id != lapImpor.LapImporID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lapImpor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LapImporExists(lapImpor.LapImporID))
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
            return View(lapImpor);
        }

        // GET: LapImpors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lapImpor = await _context.LapImpor
                .FirstOrDefaultAsync(m => m.LapImporID == id);
            if (lapImpor == null)
            {
                return NotFound();
            }

            return View(lapImpor);
        }

        // POST: LapImpors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lapImpor = await _context.LapImpor.FindAsync(id);
            _context.LapImpor.Remove(lapImpor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulatelapDropdownList(object selectedLap = null)
        {
            var lapsQuery = from d in _context.LapYear
                               orderby d.Year
                               select d;
            ViewBag.Year = new SelectList(lapsQuery.AsNoTracking(), "Year", "Year", selectedLap);
        }

        private bool LapImporExists(int id)
        {
            return _context.LapImpor.Any(e => e.LapImporID == id);
        }
    }
}
