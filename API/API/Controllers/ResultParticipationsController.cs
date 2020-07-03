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
    public class ResultParticipationsController : ControllerBase
    {
        private readonly testContext _context;

        public ResultParticipationsController(testContext context)
        {
            _context = context;
        }

        // GET: api/ResultParticipations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultParticipation>>> GetResultParticipation()
        {
            return await _context.ResultParticipation.ToListAsync();
        }

        // GET: api/ResultParticipations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultParticipation>> GetResultParticipation(int id)
        {
            var resultParticipation = await _context.ResultParticipation.FindAsync(id);

            if (resultParticipation == null)
            {
                return NotFound();
            }

            return resultParticipation;
        }

        // PUT: api/ResultParticipations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultParticipation(int id, ResultParticipation resultParticipation)
        {
            if (id != resultParticipation.IdResultParticipation)
            {
                return BadRequest();
            }

            _context.Entry(resultParticipation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultParticipationExists(id))
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

        // POST: api/ResultParticipations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ResultParticipation>> PostResultParticipation(ResultParticipation resultParticipation)
        {
            _context.ResultParticipation.Add(resultParticipation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultParticipation", new { id = resultParticipation.IdResultParticipation }, resultParticipation);
        }

        // DELETE: api/ResultParticipations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultParticipation>> DeleteResultParticipation(int id)
        {
            var resultParticipation = await _context.ResultParticipation.FindAsync(id);
            if (resultParticipation == null)
            {
                return NotFound();
            }

            _context.ResultParticipation.Remove(resultParticipation);
            await _context.SaveChangesAsync();

            return resultParticipation;
        }

        private bool ResultParticipationExists(int id)
        {
            return _context.ResultParticipation.Any(e => e.IdResultParticipation == id);
        }
    }
}
