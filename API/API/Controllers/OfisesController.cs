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
    public class OfisesController : ControllerBase
    {
        private readonly testContext _context;

        public OfisesController(testContext context)
        {
            _context = context;
        }

        // GET: api/Ofises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ofise>>> GetOfise()
        {
            return await _context.Ofise.ToListAsync();
        }

        // GET: api/Ofises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ofise>> GetOfise(int id)
        {
            var ofise = await _context.Ofise.FindAsync(id);

            if (ofise == null)
            {
                return NotFound();
            }

            return ofise;
        }

        // PUT: api/Ofises/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfise(int id, Ofise ofise)
        {
            if (id != ofise.IdOfis)
            {
                return BadRequest();
            }

            _context.Entry(ofise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfiseExists(id))
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

        // POST: api/Ofises
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Ofise>> PostOfise(Ofise ofise)
        {
            _context.Ofise.Add(ofise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfise", new { id = ofise.IdOfis }, ofise);
        }

        // DELETE: api/Ofises/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ofise>> DeleteOfise(int id)
        {
            var ofise = await _context.Ofise.FindAsync(id);
            if (ofise == null)
            {
                return NotFound();
            }

            _context.Ofise.Remove(ofise);
            await _context.SaveChangesAsync();

            return ofise;
        }

        private bool OfiseExists(int id)
        {
            return _context.Ofise.Any(e => e.IdOfis == id);
        }
    }
}
