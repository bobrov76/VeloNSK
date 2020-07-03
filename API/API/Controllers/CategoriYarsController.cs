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
    public class CategoriYarsController : ControllerBase
    {
        private readonly testContext _context;

        public CategoriYarsController(testContext context)
        {
            _context = context;
        }

        // GET: api/CategoriYars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriYars>>> GetCategoriYars()
        {
            return await _context.CategoriYars.ToListAsync();
        }

        // GET: api/CategoriYars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriYars>> GetCategoriYars(short id)
        {
            var categoriYars = await _context.CategoriYars.FindAsync(id);

            if (categoriYars == null)
            {
                return NotFound();
            }

            return categoriYars;
        }

        // PUT: api/CategoriYars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriYars(short id, CategoriYars categoriYars)
        {
            if (id != categoriYars.IdCategori)
            {
                return BadRequest();
            }

            _context.Entry(categoriYars).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriYarsExists(id))
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

        // POST: api/CategoriYars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CategoriYars>> PostCategoriYars(CategoriYars categoriYars)
        {
            _context.CategoriYars.Add(categoriYars);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriYars", new { id = categoriYars.IdCategori }, categoriYars);
        }

        // DELETE: api/CategoriYars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriYars>> DeleteCategoriYars(short id)
        {
            var categoriYars = await _context.CategoriYars.FindAsync(id);
            if (categoriYars == null)
            {
                return NotFound();
            }

            _context.CategoriYars.Remove(categoriYars);
            await _context.SaveChangesAsync();

            return categoriYars;
        }

        private bool CategoriYarsExists(short id)
        {
            return _context.CategoriYars.Any(e => e.IdCategori == id);
        }
    }
}
