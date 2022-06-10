using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
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
using System.Linq.Dynamic.Core;

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class TopKomoditisController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public TopKomoditisController(ProjectAlphaContext context)
        {
            _context = context;
        }
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
                    sqlBulkCopy.DestinationTableName = "dbo.TopKomoditi";

                    sqlBulkCopy.ColumnMappings.Add("TopKomoditiID", "TopKomoditiID");
                    sqlBulkCopy.ColumnMappings.Add("Nama", "Nama");
                    sqlBulkCopy.ColumnMappings.Add("Devisa", "Devisa");
                    sqlBulkCopy.ColumnMappings.Add("Tonnase", "Tonnase");
                    sqlBulkCopy.ColumnMappings.Add("Bulan", "Bulan");
                    sqlBulkCopy.ColumnMappings.Add("Year", "Year");



                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            ViewBag.Success = "Done";
            return View("Index");

        }
        // GET: TopKomoditis
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.TopKomoditi;
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: TopKomoditis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topKomoditi = await _context.TopKomoditi
                
                .FirstOrDefaultAsync(m => m.TopKomoditiID == id);
            if (topKomoditi == null)
            {
                return NotFound();
            }

            return View(topKomoditi);
        }

        // GET: TopKomoditis/Create
        public IActionResult Create()
        {
            ViewData["LapYearID"] = new SelectList(_context.LapYear, "LapYearID", "LapYearID");
            return View();
        }

        // POST: TopKomoditis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopKomoditiID,Nama,Devisa,Tonnase,Bulan,Year")] TopKomoditi topKomoditi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topKomoditi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(topKomoditi);
        }

        // GET: TopKomoditis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topKomoditi = await _context.TopKomoditi.FindAsync(id);
            if (topKomoditi == null)
            {
                return NotFound();
            }
            
            return View(topKomoditi);
        }

        // POST: TopKomoditis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopKomoditiID,Nama,Devisa,Tonnase,Bulan,LapYearID")] TopKomoditi topKomoditi)
        {
            if (id != topKomoditi.TopKomoditiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topKomoditi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopKomoditiExists(topKomoditi.TopKomoditiID))
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
            
            return View(topKomoditi);
        }

        // GET: TopKomoditis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topKomoditi = await _context.TopKomoditi
                
                .FirstOrDefaultAsync(m => m.TopKomoditiID == id);
            if (topKomoditi == null)
            {
                return NotFound();
            }

            return View(topKomoditi);
        }

        // POST: TopKomoditis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topKomoditi = await _context.TopKomoditi.FindAsync(id);
            _context.TopKomoditi.Remove(topKomoditi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopKomoditiExists(int id)
        {
            return _context.TopKomoditi.Any(e => e.TopKomoditiID == id);
        }
    }
}
