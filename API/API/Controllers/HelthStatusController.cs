using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelthStatusController : ControllerBase
    {
        private readonly testContext _context;

        public HelthStatusController(testContext context)
        {
            _context = context;
        }

        // GET: api/HelthStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HelthStatus>>> GetHelthStatus()
        {
            return await _context.HelthStatus.ToListAsync();
        }

        // GET: api/HelthStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HelthStatus>> GetHelthStatus(short id)
        {
            var helthStatus = await _context.HelthStatus.FindAsync(id);

            if (helthStatus == null)
            {
                return NotFound();
            }

            return helthStatus;
        }

        // PUT: api/HelthStatus/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelthStatus(short id, HelthStatus helthStatus)
        {
            if (id != helthStatus.IdHealth)
            {
                return BadRequest();
            }

            _context.Entry(helthStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelthStatusExists(id))
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

        // POST: api/HelthStatus
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HelthStatus>> PostHelthStatus(HelthStatus helthStatus)
        {
            _context.HelthStatus.Add(helthStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelthStatus", new { id = helthStatus.IdHealth }, helthStatus);
        }

        // DELETE: api/HelthStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HelthStatus>> DeleteHelthStatus(short id)
        {
            var helthStatus = await _context.HelthStatus.FindAsync(id);
            if (helthStatus == null)
            {
                return NotFound();
            }

            _context.HelthStatus.Remove(helthStatus);
            await _context.SaveChangesAsync();

            return helthStatus;
        }

        private bool HelthStatusExists(short id)
        {
            return _context.HelthStatus.Any(e => e.IdHealth == id);
        }
    }
}
