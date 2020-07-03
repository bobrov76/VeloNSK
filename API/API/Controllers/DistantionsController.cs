using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DistantionsController : ControllerBase
    {
        private readonly testContext _context;

        public DistantionsController(testContext context)
        {
            _context = context;
        }

        // GET: api/Distantions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Distantion>>> GetDistantion()
        {
            return await _context.Distantion.ToListAsync();
        }

        // GET: api/Distantions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Distantion>> GetDistantion(int id)
        {
            var distantion = await _context.Distantion.FindAsync(id);

            if (distantion == null)
            {
                return NotFound();
            }

            return distantion;
        }

        // PUT: api/Distantions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistantion(int id, Distantion distantion)
        {
            if (id != distantion.IdDistantion)
            {
                return BadRequest();
            }

            _context.Entry(distantion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistantionExists(id))
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

        // POST: api/Distantions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Distantion>> PostDistantion(Distantion distantion)
        {
            _context.Distantion.Add(distantion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistantion", new { id = distantion.IdDistantion }, distantion);
        }

        // DELETE: api/Distantions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Distantion>> DeleteDistantion(int id)
        {
            var distantion = await _context.Distantion.FindAsync(id);
            if (distantion == null)
            {
                return NotFound();
            }

            _context.Distantion.Remove(distantion);
            await _context.SaveChangesAsync();

            return distantion;
        }

        private bool DistantionExists(int id)
        {
            return _context.Distantion.Any(e => e.IdDistantion == id);
        }
    }
}
