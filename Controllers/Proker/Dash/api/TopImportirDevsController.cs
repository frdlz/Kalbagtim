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
    public class TopImportirDevsController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public TopImportirDevsController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult PostTopImportirDev()
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
            var assistData = (from assist in _context.TopImportirDev select assist);

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
        // GET: api/TopImportirDevs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopImportirDev>>> GetTopImportirDev()
        {
            return await _context.TopImportirDev.ToListAsync();
        }

        // GET: api/TopImportirDevs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopImportirDev>> GetTopImportirDev(int id)
        {
            var topImportirDev = await _context.TopImportirDev.FindAsync(id);

            if (topImportirDev == null)
            {
                return NotFound();
            }

            return topImportirDev;
        }

        // PUT: api/TopImportirDevs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopImportirDev(int id, TopImportirDev topImportirDev)
        {
            if (id != topImportirDev.TopImportirDevID)
            {
                return BadRequest();
            }

            _context.Entry(topImportirDev).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopImportirDevExists(id))
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

        // POST: api/TopImportirDevs
       

        // DELETE: api/TopImportirDevs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopImportirDev>> DeleteTopImportirDev(int id)
        {
            var topImportirDev = await _context.TopImportirDev.FindAsync(id);
            if (topImportirDev == null)
            {
                return NotFound();
            }

            _context.TopImportirDev.Remove(topImportirDev);
            await _context.SaveChangesAsync();

            return topImportirDev;
        }

        private bool TopImportirDevExists(int id)
        {
            return _context.TopImportirDev.Any(e => e.TopImportirDevID == id);
        }
    }
}
