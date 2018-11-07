using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2ndVueApp.Models;
using System.Net;

namespace _2ndVueApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Earnings")]
    public class EarningsController : Controller
    {
        private readonly PTContext _context;

        public EarningsController(PTContext context)
        {
            _context = context;
        }

        // GET: api/Earnings
        [HttpGet]
        public IEnumerable<Object> GetEarnings()
        {
            var earnings = (from eg in _context.Earnings
                            join e in _context.Earners on eg.Earner.EarnerId equals e.EarnerId
                            select new
                            {
                                earnedDate = eg.EarnedDate.ToString("MM/dd/yyyy"),
                                name = e.LastName + ", " + e.FirstName,
                                points = eg.Points
                            }).Take(10);

            return earnings;
        }

        // GET: api/Earnings/Page/2/RowsPerPage/10/Sort/Name
        [HttpGet("Page/{pageNum}/RowsPerPage/{rowsPerPage}/Sort/{sortField}")]
        public IEnumerable<Object> GetEarningsPage([FromRoute] int pageNum, [FromRoute] int rowsPerPage, [FromRoute] string sortField)
        {
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[GetEarningsPage]";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PageNum", pageNum));
            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RowsPerPage", rowsPerPage));
            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SortField", sortField));
            List<Object> earnings = new List<Object>();

            try
            {

                _context.Database.GetDbConnection().Open();
                // Run the sproc 
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    earnings.Add(new
                    {
                        earnedDate = Convert.ToDateTime(reader["EarnedDate"]).ToString("MM/dd/yyyy"),
                        name = reader["Name"].ToString(),
                        points = reader["Points"]
                    });
                }

            }
            finally
            {
                _context.Database.GetDbConnection().Close();
            }

            return earnings;
        }

        // GET: api/Earnings/Count
        [HttpGet("Count")]
        public int GetEarningsCount()
        {
            int count = (from eg in _context.Earnings
                            select eg).Count();

            return count;
        }

        // GET: api/Earnings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEarning([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var earning = await _context.Earnings.SingleOrDefaultAsync(m => m.EarningId == id);

            if (earning == null)
            {
                return NotFound();
            }

            return Ok(earning);
        }

        // PUT: api/Earnings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEarning([FromRoute] int id, [FromBody] Earning earning)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != earning.EarningId)
            {
                return BadRequest();
            }

            _context.Entry(earning).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EarningExists(id))
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

        // POST: api/Earnings
        [HttpPost]
        public async Task<IActionResult> PostEarning([FromBody] Earning earning)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Get the points value from the Rules table
                var points = (from r in _context.Rules
                              where r.RuleId == earning.RuleId
                              select r.Value
                                  ).FirstOrDefault();

                // Set the points on the passed-in object
                earning.Points = Convert.ToInt32(points);

                _context.Earnings.Add(earning);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEarning", new { id = earning.EarningId }, earning);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        // DELETE: api/Earnings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEarning([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var earning = await _context.Earnings.SingleOrDefaultAsync(m => m.EarningId == id);
            if (earning == null)
            {
                return NotFound();
            }

            _context.Earnings.Remove(earning);
            await _context.SaveChangesAsync();

            return Ok(earning);
        }

        private bool EarningExists(int id)
        {
            return _context.Earnings.Any(e => e.EarningId == id);
        }
    }
}