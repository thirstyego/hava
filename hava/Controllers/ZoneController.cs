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
    public class ZoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Zone/home/2
        // GET all Zones associated with Home
        [HttpGet("home/{homeId}")]
        public async Task<ActionResult<IEnumerable<Zone>>> GetZones(int homeId)
        {
          if (_context.Zones == null)
          {
              return NotFound();
          }

          var zonesInHome = await _context.Zones.Where(z => z.HomeId == homeId).ToListAsync();
          return zonesInHome;
        }

        // GET: api/Zone/5
        // GET Zone by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZone(int id)
        {
          if (id == null)
          {
              return NotFound();
          }
            // var zone = await _context.Zones
            //     .Where(z => z.Id == id)
            //     .FirstOrDefaultAsync();
            var zone = await _context.Zones.FindAsync(id);

            if (zone == null)
            {
                return NotFound();
            }

            return zone;
        }

        // PUT: api/Zone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutZone(ZonePut zonePut)
        {
            if (zonePut == null)
            {
                return BadRequest();
            }

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          var zone = new Zone()
          {
              Id = zonePut.Id,
              Name = zonePut.Name,
              HomeId = zonePut.HomeId
          };
          
            _context.Entry(zone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.Id))
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
        public async Task<ActionResult<Zone>> PostZone(ZonePost zonePost)
        {
          if (zonePost == null)
          {
              return BadRequest();
          }

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          var zone = new Zone()
          {
              Name = zonePost.Name,
              HomeId = zonePost.HomeId
          };
              
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZone", new { id = zone.Id }, zone);
        }

        // DELETE: api/Zone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(int id)
        {
            if (id == null)
            {
                return BadRequest();
                // return NotFound();
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
