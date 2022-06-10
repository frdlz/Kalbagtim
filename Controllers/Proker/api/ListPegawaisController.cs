using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAlpha.Data;
using ProjectAlpha.Models.Proker;
using System.Linq.Dynamic.Core;

namespace ProjectAlpha.Controllers.Proker.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListPegawaisController : ControllerBase
    {
        private readonly ProjectAlphaContext _context;

        public ListPegawaisController(ProjectAlphaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult PostListPegawai()
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
            var assistData = (from assist in _context.ListPegawai select assist);

            if (!(String.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                assistData = assistData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                assistData = assistData.Where(a =>
                                                 a.NamaPegawai.ToString().Contains(searchValue));

            }
            recordTotal = assistData.Count();
            var data = assistData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, data = data, recordFiltered = recordTotal, recordTotal = recordTotal };
            return Ok(jsonData);


        }
        // GET: api/ListPegawais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListPegawai>>> GetListPegawai()
        {
            return await _context.ListPegawai.ToListAsync();
        }

        // GET: api/ListPegawais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListPegawai>> GetListPegawai(string id)
        {
            var listPegawai = await _context.ListPegawai.FindAsync(id);

            if (listPegawai == null)
            {
                return NotFound();
            }

            return listPegawai;
        }

        // PUT: api/ListPegawais/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListPegawai(string id, ListPegawai listPegawai)
        {
            if (id != listPegawai.ListPegawaiID)
            {
                return BadRequest();
            }

            _context.Entry(listPegawai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListPegawaiExists(id))
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

        // POST: api/ListPegawais
        

        // DELETE: api/ListPegawais/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ListPegawai>> DeleteListPegawai(string id)
        {
            var listPegawai = await _context.ListPegawai.FindAsync(id);
            if (listPegawai == null)
            {
                return NotFound();
            }

            _context.ListPegawai.Remove(listPegawai);
            await _context.SaveChangesAsync();

            return listPegawai;
        }

        private bool ListPegawaiExists(string id)
        {
            return _context.ListPegawai.Any(e => e.ListPegawaiID == id);
        }
    }
}
