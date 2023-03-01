using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apichipper.Data;
using apichipper.Models;

namespace apichipper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentEnvironmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CurrentEnvironmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CurrentEnvironment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrentEnvironment>>> GetCurrentEnvironments()
        {
          if (_context.CurrentEnvironments == null)
          {
              return NotFound();
          }
            return await _context.CurrentEnvironments.ToListAsync();
        }

        // GET: api/CurrentEnvironment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentEnvironment>> GetCurrentEnvironment(int id)
        {
          if (_context.CurrentEnvironments == null)
          {
              return NotFound();
          }
            var currentEnvironment = await _context.CurrentEnvironments.FindAsync(id);

            if (currentEnvironment == null)
            {
                return NotFound();
            }

            return currentEnvironment;
        }

        // PUT: api/CurrentEnvironment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrentEnvironment(int id, CurrentEnvironment currentEnvironment)
        {
            if (id != currentEnvironment.Id)
            {
                return BadRequest();
            }

            _context.Entry(currentEnvironment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrentEnvironmentExists(id))
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

        // POST: api/CurrentEnvironment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurrentEnvironment>> PostCurrentEnvironment(CurrentEnvironment currentEnvironment)
        {
          if (_context.CurrentEnvironments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CurrentEnvironments'  is null.");
          }
            _context.CurrentEnvironments.Add(currentEnvironment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrentEnvironment", new { id = currentEnvironment.Id }, currentEnvironment);
        }

        // DELETE: api/CurrentEnvironment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrentEnvironment(int id)
        {
            if (_context.CurrentEnvironments == null)
            {
                return NotFound();
            }
            var currentEnvironment = await _context.CurrentEnvironments.FindAsync(id);
            if (currentEnvironment == null)
            {
                return NotFound();
            }

            _context.CurrentEnvironments.Remove(currentEnvironment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrentEnvironmentExists(int id)
        {
            return (_context.CurrentEnvironments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
