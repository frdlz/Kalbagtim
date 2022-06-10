using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker.Dash;
using System.Linq.Dynamic.Core;

namespace ProjectAlpha.Controllers.Proker.Dash.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopKomoditisController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public TopKomoditisController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult PostTopKomoditi()
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
            var assistData = (from assist in _context.TopKomoditi select assist);

            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.Year.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        // GET: api/TopKomoditis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopKomoditi>>> GetTopKomoditi()
        {
            return await _context.TopKomoditi.ToListAsync();
        }

        // GET: api/TopKomoditis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopKomoditi>> GetTopKomoditi(int id)
        {
            var topKomoditi = await _context.TopKomoditi.FindAsync(id);

            if (topKomoditi == null)
            {
                return NotFound();
            }

            return topKomoditi;
        }

        // PUT: api/TopKomoditis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopKomoditi(int id, TopKomoditi topKomoditi)
        {
            if (id != topKomoditi.TopKomoditiID)
            {
                return BadRequest();
            }

            _context.Entry(topKomoditi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopKomoditiExists(id))
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

        // POST: api/TopKomoditis
       

        // DELETE: api/TopKomoditis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopKomoditi>> DeleteTopKomoditi(int id)
        {
            var topKomoditi = await _context.TopKomoditi.FindAsync(id);
            if (topKomoditi == null)
            {
                return NotFound();
            }

            _context.TopKomoditi.Remove(topKomoditi);
            await _context.SaveChangesAsync();

            return topKomoditi;
        }

        private bool TopKomoditiExists(int id)
        {
            return _context.TopKomoditi.Any(e => e.TopKomoditiID == id);
        }
    }
}
