using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Office;
using System.Linq.Dynamic.Core;

namespace ProjectAlpha.Controllers.Proker.Office.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosBarangsController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public PosBarangsController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult GetAssisting()
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
            var assistData = (from assist in _context.LapImpor select assist);
            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.Year.Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        // GET: api/PosBarangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosBarang>>> GetPosBarang()
        {
            return await _context.PosBarang.ToListAsync();
        }

        // GET: api/PosBarangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PosBarang>> GetPosBarang(string id)
        {
            var posBarang = await _context.PosBarang.FindAsync(id);

            if (posBarang == null)
            {
                return NotFound();
            }

            return posBarang;
        }

        // PUT: api/PosBarangs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosBarang(string id, PosBarang posBarang)
        {
            if (id != posBarang.PosBarangID)
            {
                return BadRequest();
            }

            _context.Entry(posBarang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosBarangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PosBarangs
        

        // DELETE: api/PosBarangs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PosBarang>> DeletePosBarang(string id)
        {
            var posBarang = await _context.PosBarang.FindAsync(id);
            if (posBarang == null)
            {
                return NotFound();
            }

            _context.PosBarang.Remove(posBarang);
            await _context.SaveChangesAsync();

            return posBarang;
        }

        private bool PosBarangExists(string id)
        {
            return _context.PosBarang.Any(e => e.PosBarangID == id);
        }
    }
}
