
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

namespace ProjectAlpha.Controllers.Proker.Dash
{
    public class TopImporBMsController : Controller
    {
        private readonly ProjectAlphaContext _context;

        public TopImporBMsController(ProjectAlphaContext context)
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
                    sqlBulkCopy.DestinationTableName = "dbo.TopImporBM";

                    sqlBulkCopy.ColumnMappings.Add("TopImporBMID", "TopImporBMID");
                    sqlBulkCopy.ColumnMappings.Add("Bulan", "Bulan");
                    sqlBulkCopy.ColumnMappings.Add("NamaImortir", "NamaImortir");
                    sqlBulkCopy.ColumnMappings.Add("BM", "BM");
                    sqlBulkCopy.ColumnMappings.Add("Year", "Year");
                   


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            ViewBag.Success = "Done";
            return View("Index");

        }
        // GET: TopImporBMs
        public async Task<IActionResult> Index()
        {
            var ProjectAlphaContext = _context.TopImporBM;
            return View(await ProjectAlphaContext.ToListAsync());
        }

        // GET: TopImporBMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topImporBM = await _context.TopImporBM
               
                .FirstOrDefaultAsync(m => m.TopImporBMID == id);
            if (topImporBM == null)
            {
                return NotFound();
            }

            return View(topImporBM);
        }

        // GET: TopImporBMs/Create
        public IActionResult Create()
        {
            ViewData["LapYearID"] = new SelectList(_context.LapYear, "LapYearID", "LapYearID");
            return View();
        }

        // POST: TopImporBMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopImporBMID,Bulan,NamaImortir,BM,Year")] TopImporBM topImporBM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topImporBM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(topImporBM);
        }

        // GET: TopImporBMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topImporBM = await _context.TopImporBM.FindAsync(id);
            if (topImporBM == null)
            {
                return NotFound();
            }
            
            return View(topImporBM);
        }

        // POST: TopImporBMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopImporBMID,Bulan,NamaImortir,BM,LapYearID")] TopImporBM topImporBM)
        {
            if (id != topImporBM.TopImporBMID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topImporBM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopImporBMExists(topImporBM.TopImporBMID))
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
            
            return View(topImporBM);
        }

        // GET: TopImporBMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topImporBM = await _context.TopImporBM
               
                .FirstOrDefaultAsync(m => m.TopImporBMID == id);
            if (topImporBM == null)
            {
                return NotFound();
            }

            return View(topImporBM);
        }

        // POST: TopImporBMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topImporBM = await _context.TopImporBM.FindAsync(id);
            _context.TopImporBM.Remove(topImporBM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopImporBMExists(int id)
        {
            return _context.TopImporBM.Any(e => e.TopImporBMID == id);
        }
    }
}
