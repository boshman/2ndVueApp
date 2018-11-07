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
    [Route("api/Rules")]
    public class RulesController : Controller
    {
        private readonly PTContext _context;

        public RulesController(PTContext context)
        {
            _context = context;
        }

        // GET: api/Rules
        [HttpGet]
        public IEnumerable<Rule> GetRules()
        {
            return _context.Rules;
        }

        [HttpGet("semantic_dd")]
        public Object GetRulesForSemanticDropdown()
        {
            var rules = (from r in _context.Rules
                           select new
                           {
                               name = r.Name,
                               value = r.RuleId
                           });

            var rtnObject = new { success = "true", results = rules };

            return rtnObject;
        }

        // GET: api/Rules/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rule = await _context.Rules.SingleOrDefaultAsync(m => m.RuleId == id);

            if (rule == null)
            {
                return NotFound();
            }

            return Ok(rule);
        }

        // PUT: api/Rules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRule([FromRoute] int id, [FromBody] Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rule.RuleId)
            {
                return BadRequest();
            }

            _context.Entry(rule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleExists(id))
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

        // POST: api/Rules
        [HttpPost]
        public async Task<IActionResult> PostRule([FromBody] Rule rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Rules.Add(rule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRule", new { id = rule.RuleId }, rule);
        }

        // DELETE: api/Rules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rule = await _context.Rules.SingleOrDefaultAsync(m => m.RuleId == id);
            if (rule == null)
            {
                return NotFound();
            }

            _context.Rules.Remove(rule);
            await _context.SaveChangesAsync();

            return Ok(rule);
        }

        private bool RuleExists(int id)
        {
            return _context.Rules.Any(e => e.RuleId == id);
        }
    }
}