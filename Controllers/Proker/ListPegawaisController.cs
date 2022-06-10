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
using ProjectAlpha.Models.Proker;

namespace ProjectAlpha.Controllers.Proker
{
    public class ListPegawaisController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public ListPegawaisController(ProjectAlphaContext context)
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
                    sqlBulkCopy.DestinationTableName = "dbo.ListPegawai";

                    sqlBulkCopy.ColumnMappings.Add("ListPegawaiID", "ListPegawaiID");
                    sqlBulkCopy.ColumnMappings.Add("NamaPegawai", "NamaPegawai");
                    



                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            ViewBag.Success = "Done";
            return View("Index");

        }
        // GET: ListPegawais
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.ListPegawai;
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: ListPegawais/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPegawai = await _context.ListPegawai
                
                .FirstOrDefaultAsync(m => m.ListPegawaiID == id);
            if (listPegawai == null)
            {
                return NotFound();
            }

            return View(listPegawai);
        }

        // GET: ListPegawais/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: ListPegawais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListPegawaiID,NamaPegawai")] ListPegawai listPegawai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listPegawai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(listPegawai);
        }

        // GET: ListPegawais/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPegawai = await _context.ListPegawai.FindAsync(id);
            if (listPegawai == null)
            {
                return NotFound();
            }
            PopulateJabatanDropdownList(listPegawai.JabatanID);
            PopulatePangkatDropdownList(listPegawai.PangkatGolID);
            return View(listPegawai);
        }

        // POST: ListPegawais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ListPegawaiID,NamaPegawai,JabatanID,PangkatGolID")] ListPegawai listPegawai)
        {
            if (id != listPegawai.ListPegawaiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listPegawai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListPegawaiExists(listPegawai.ListPegawaiID))
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
            PopulateJabatanDropdownList(listPegawai.JabatanID);
            PopulatePangkatDropdownList(listPegawai.PangkatGolID);
            return View(listPegawai);
        }

        // GET: ListPegawais/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPegawai = await _context.ListPegawai
                
                .FirstOrDefaultAsync(m => m.ListPegawaiID == id);
            if (listPegawai == null)
            {
                return NotFound();
            }

            return View(listPegawai);
        }

        // POST: ListPegawais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var listPegawai = await _context.ListPegawai.FindAsync(id);
            _context.ListPegawai.Remove(listPegawai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListPegawaiExists(string id)
        {
            return _context.ListPegawai.Any(e => e.ListPegawaiID == id);
        }
        private void PopulateJabatanDropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.Jabatan
                           orderby d.NamaJabatan
                           select d;
            ViewBag.Jabatan = new SelectList(p2sQuery

                .AsNoTracking(), "JabatanID", "NamaJabatan", selectedPetugasP2);
        }
        private void PopulatePangkatDropdownList(object selectedPetugasP2 = null)
        {
            var p2sQuery = from d in _context.PangkatGol
                           orderby d.NamaPangkat
                           select d;
            ViewBag.Pangkat = new SelectList(p2sQuery

                .AsNoTracking(), "PangkatGolID", "NamaPangkat", selectedPetugasP2);
        }
    }
}
