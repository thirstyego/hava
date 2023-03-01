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
    public class ZoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Zone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
        {
          if (_context.Zones == null)
          {
              return NotFound();
          }

          var HomeId = 1;

          var zonesInHome = await _context.Zones.Where(z => z.HomeId == HomeId).ToListAsync();
          return zonesInHome;
        }

        // GET: api/Zone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZone(int id)
        {
          if (_context.Zones == null)
          {
              return NotFound();
          }
            var zone = await _context.Zones.FindAsync(id);

            if (zone == null)
            {
                return NotFound();
            }

            return zone;
        }

        // PUT: api/Zone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZone(int id, Zone zone)
        {
            if (id != zone.Id)
            {
                return BadRequest();
            }

            _context.Entry(zone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(id))
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

        // POST: api/Zone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zone>> PostZone(Zone zone)
        {
          if (_context.Zones == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Zones'  is null.");
          }
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZone", new { id = zone.Id }, zone);
        }

        // DELETE: api/Zone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(int id)
        {
            if (_context.Zones == null)
            {
                return NotFound();
            }
            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }

            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZoneExists(int id)
        {
            return (_context.Zones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
