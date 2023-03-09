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
    public class DeviceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Zone/Device
        // GET Devices associated with Zone
        [HttpGet("Zone/{zoneId}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices(int zoneId)
        {
          if (zoneId == null)
          {
              return BadRequest();
          }

          var devicesInZone = await _context.Devices.Where(d => d.ZoneId == zoneId).ToListAsync();
          return devicesInZone;
        }

        // GET: api/Device/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
          if (_context.Devices == null)
          {
              return NotFound();
          }
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // PUT: api/Device/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutDevice(DevicePut devicePut)
        {
            if (devicePut == null)
            {
                return BadRequest();
            }

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

            var device = new Device()
            {
                Id = devicePut.Id,
                BatteryPercentage = devicePut.BatteryPercentage,
                Status = devicePut.Status,
                ZoneId = devicePut.ZoneId
            };

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.Id))
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

        // POST: api/Device
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(DevicePost devicePost)
        {
          if (devicePost == null)
          {
              return BadRequest();
          }
          
          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

            var device = new Device()
            {
                BatteryPercentage = devicePost.BatteryPercentage,
                Status = devicePost.Status,
                ZoneId = devicePost.ZoneId
            };
            
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        // DELETE: api/Device/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(int id)
        {
            return (_context.Devices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
