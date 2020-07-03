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
    public class CompetentionsController : ControllerBase
    {
        private readonly testContext _context;

        public CompetentionsController(testContext context)
        {
            _context = context;
        }

        // GET: api/Competentions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competentions>>> GetCompetentions()
        {
            return await _context.Competentions.ToListAsync();
        }

        // GET: api/Competentions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Competentions>> GetCompetentions(int id)
        {
            var competentions = await _context.Competentions.FindAsync(id);

            if (competentions == null)
            {
                return NotFound();
            }

            return competentions;
        }

        // PUT: api/Competentions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetentions(int id, Competentions competentions)
        {
            if (id != competentions.IdCompetentions)
            {
                return BadRequest();
            }

            _context.Entry(competentions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetentionsExists(id))
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

        // POST: api/Competentions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Competentions>> PostCompetentions(Competentions competentions)
        {
            _context.Competentions.Add(competentions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetentions", new { id = competentions.IdCompetentions }, competentions);
        }

        // DELETE: api/Competentions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Competentions>> DeleteCompetentions(int id)
        {
            var competentions = await _context.Competentions.FindAsync(id);
            if (competentions == null)
            {
                return NotFound();
            }

            _context.Competentions.Remove(competentions);
            await _context.SaveChangesAsync();

            return competentions;
        }

        private bool CompetentionsExists(int id)
        {
            return _context.Competentions.Any(e => e.IdCompetentions == id);
        }
    }
}
