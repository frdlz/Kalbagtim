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
    public class TopImporBMsController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public TopImporBMsController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult PostTopImporBM()
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
            var assistData = (from assist in _context.TopImporBM select assist);

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
        // GET: api/TopImporBMs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopImporBM>>> GetTopImporBM()
        {
            return await _context.TopImporBM.ToListAsync();
        }

        // GET: api/TopImporBMs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopImporBM>> GetTopImporBM(int id)
        {
            var topImporBM = await _context.TopImporBM.FindAsync(id);

            if (topImporBM == null)
            {
                return NotFound();
            }

            return topImporBM;
        }

        // PUT: api/TopImporBMs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopImporBM(int id, TopImporBM topImporBM)
        {
            if (id != topImporBM.TopImporBMID)
            {
                return BadRequest();
            }

            _context.Entry(topImporBM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopImporBMExists(id))
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

        // POST: api/TopImporBMs
       

        // DELETE: api/TopImporBMs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopImporBM>> DeleteTopImporBM(int id)
        {
            var topImporBM = await _context.TopImporBM.FindAsync(id);
            if (topImporBM == null)
            {
                return NotFound();
            }

            _context.TopImporBM.Remove(topImporBM);
            await _context.SaveChangesAsync();

            return topImporBM;
        }

        private bool TopImporBMExists(int id)
        {
            return _context.TopImporBM.Any(e => e.TopImporBMID == id);
        }
    }
}
