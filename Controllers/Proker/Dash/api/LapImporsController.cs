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
    public class LapImporsController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public LapImporsController(ProjectAlphaContext context)
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
        // GET: api/LapImpors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LapImpor>>> GetLapImpor()
        {
            return await _context.LapImpor.ToListAsync();
        }

        // GET: api/LapImpors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LapImpor>> GetLapImpor(int id)
        {
            var lapImpor = await _context.LapImpor.FindAsync(id);

            if (lapImpor == null)
            {
                return NotFound();
            }

            return lapImpor;
        }

        // PUT: api/LapImpors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLapImpor(int id, LapImpor lapImpor)
        {
            if (id != lapImpor.LapImporID)
            {
                return BadRequest();
            }

            _context.Entry(lapImpor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LapImporExists(id))
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

        // POST: api/LapImpors
       
        // DELETE: api/LapImpors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LapImpor>> DeleteLapImpor(int id)
        {
            var lapImpor = await _context.LapImpor.FindAsync(id);
            if (lapImpor == null)
            {
                return NotFound();
            }

            _context.LapImpor.Remove(lapImpor);
            await _context.SaveChangesAsync();

            return lapImpor;
        }

        private bool LapImporExists(int id)
        {
            return _context.LapImpor.Any(e => e.LapImporID == id);
        }
    }
}
