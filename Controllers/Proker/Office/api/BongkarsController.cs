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
using ProjectAlpha.Models.Proker.Office.ViewModel;

namespace ProjectAlpha.Controllers.Proker.Office.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BongkarsController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public BongkarsController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult GetBongkars()
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
            var assistData = (from assist in _context.Bongkar select assist);

            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.Hal.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);



        }
        // GET: api/Bongkars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bongkar>>> GetBongkar()
        {
            return await _context.Bongkar.ToListAsync();
        }

        // GET: api/Bongkars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bongkar>> GetBongkar(string id)
        {
            var bongkar = await _context.Bongkar.FindAsync(id);

            if (bongkar == null)
            {
                return NotFound();
            }

            return bongkar;
        }

        // PUT: api/Bongkars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBongkar(string id, Bongkar bongkar)
        {
            if (id != bongkar.BongkarID)
            {
                return BadRequest();
            }

            _context.Entry(bongkar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BongkarExists(id))
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

        // POST: api/Bongkars
        [HttpPost]
        public async Task<ActionResult<Bongkar>> PostBongkar(Bongkar bongkar)
        {
            _context.Bongkar.Add(bongkar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBongkar", new { id = bongkar.BongkarID }, bongkar);
        }

        // DELETE: api/Bongkars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bongkar>> DeleteBongkar(string id)
        {
            var bongkar = await _context.Bongkar.FindAsync(id);
            if (bongkar == null)
            {
                return NotFound();
            }

            _context.Bongkar.Remove(bongkar);
            await _context.SaveChangesAsync();

            return bongkar;
        }

        private bool BongkarExists(string id)
        {
            return _context.Bongkar.Any(e => e.BongkarID == id);
        }
    }
}
