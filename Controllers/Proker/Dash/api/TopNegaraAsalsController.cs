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
    public class TopNegaraAsalsController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public TopNegaraAsalsController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult PostTopNegaraAsal()
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
            var assistData = (from assist in _context.TopNegaraAsal select assist);

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
        // GET: api/TopNegaraAsals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopNegaraAsal>>> GetTopNegaraAsal()
        {
            return await _context.TopNegaraAsal.ToListAsync();
        }

        // GET: api/TopNegaraAsals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopNegaraAsal>> GetTopNegaraAsal(int id)
        {
            var topNegaraAsal = await _context.TopNegaraAsal.FindAsync(id);

            if (topNegaraAsal == null)
            {
                return NotFound();
            }

            return topNegaraAsal;
        }

        // PUT: api/TopNegaraAsals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopNegaraAsal(int id, TopNegaraAsal topNegaraAsal)
        {
            if (id != topNegaraAsal.TopNegaraAsalID)
            {
                return BadRequest();
            }

            _context.Entry(topNegaraAsal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopNegaraAsalExists(id))
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

        // POST: api/TopNegaraAsals
        

        // DELETE: api/TopNegaraAsals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopNegaraAsal>> DeleteTopNegaraAsal(int id)
        {
            var topNegaraAsal = await _context.TopNegaraAsal.FindAsync(id);
            if (topNegaraAsal == null)
            {
                return NotFound();
            }

            _context.TopNegaraAsal.Remove(topNegaraAsal);
            await _context.SaveChangesAsync();

            return topNegaraAsal;
        }

        private bool TopNegaraAsalExists(int id)
        {
            return _context.TopNegaraAsal.Any(e => e.TopNegaraAsalID == id);
        }
    }
}
