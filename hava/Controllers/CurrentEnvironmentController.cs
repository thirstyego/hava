using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hava.Data;
using hava.Models;

namespace hava.Controllers
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

        
        // GET: api/Zone/CurrentEnvironment
        // GET CurrentEnvironments associated wih Zone
        [HttpGet("Zone/{zoneId}")]
        public async Task<ActionResult<IEnumerable<CurrentEnvironment>>> GetCurrentEnvironments(int zoneId)
        {
          if (zoneId == null)
          {
              return BadRequest();
          }
          
          var currentEnvironmentsInZone = await _context.CurrentEnvironments.Where(d => d.ZoneId == zoneId).ToListAsync();

          return currentEnvironmentsInZone;
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
        [HttpPut]
        public async Task<IActionResult> PutCurrentEnvironment(CurrentEnvironmentPut currentEnvironmentPut)
        {
            if (currentEnvironmentPut == null)
            {
                return BadRequest();
            }
            
          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          var currentEnvironment = new CurrentEnvironment()
          { 
              Id = currentEnvironmentPut.Id,
            Name = currentEnvironmentPut.Name,
            Temperature = currentEnvironmentPut.Temperature,
            TargetTemperature = currentEnvironmentPut.TargetTemperature,
            Humidity = currentEnvironmentPut.Humidity,
            TargetHumidity = currentEnvironmentPut.TargetTemperature,
            Mode = currentEnvironmentPut.Mode,
            Date = DateTime.Now.ToString(),
            ZoneId = currentEnvironmentPut.ZoneId
          };

            _context.Entry(currentEnvironment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrentEnvironmentExists(currentEnvironment.Id))
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
        public async Task<ActionResult<CurrentEnvironment>> PostCurrentEnvironment(CurrentEnvironmentPost currentEnvironmentPost)
        {
          if (currentEnvironmentPost == null)
          {
              return BadRequest();
          }

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          var currentEnvironment = new CurrentEnvironment()
          {
            Name = currentEnvironmentPost.Name,
            Temperature = currentEnvironmentPost.Temperature,
            TargetTemperature = currentEnvironmentPost.TargetTemperature,
            Humidity = currentEnvironmentPost.Humidity,
            TargetHumidity = currentEnvironmentPost.TargetTemperature,
            Mode = currentEnvironmentPost.Mode,
            Date = DateTime.Now.ToString(),
            ZoneId = currentEnvironmentPost.ZoneId
          };
              
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
