using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2ndVueApp.Models;

namespace _2ndVueApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Earners")]
    public class EarnersController : Controller
    {
        private readonly PTContext _context;

        public EarnersController(PTContext context)
        {
            _context = context;
        }

        // GET: api/Earners
        [HttpGet]
        public IEnumerable<Earner> GetEarners()
        {
            return _context.Earners;
        }

        [HttpGet("semantic_dd")]
        public Object GetEarnersForSemanticDropdown()
        {
            var earners = (from e in _context.Earners
                           select new
                           {
                               name = e.LastName + ", " + e.FirstName,
                               value = e.EarnerId
                           });

            var rtnObject = new { success = "true", results = earners };

            return rtnObject;
        }

        // GET: api/Earners/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEarner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var earner = await _context.Earners.SingleOrDefaultAsync(m => m.EarnerId == id);

            if (earner == null)
            {
                return NotFound();
            }

            return Ok(earner);
        }

        // PUT: api/Earners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEarner([FromRoute] int id, [FromBody] Earner earner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != earner.EarnerId)
            {
                return BadRequest();
            }

            _context.Entry(earner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EarnerExists(id))
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

        // POST: api/Earners
        [HttpPost]
        public async Task<IActionResult> PostEarner([FromBody] Earner earner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Earners.Add(earner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEarner", new { id = earner.EarnerId }, earner);
        }

        // DELETE: api/Earners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEarner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var earner = await _context.Earners.SingleOrDefaultAsync(m => m.EarnerId == id);
            if (earner == null)
            {
                return NotFound();
            }

            _context.Earners.Remove(earner);
            await _context.SaveChangesAsync();

            return Ok(earner);
        }

        private bool EarnerExists(int id)
        {
            return _context.Earners.Any(e => e.EarnerId == id);
        }
    }
}
