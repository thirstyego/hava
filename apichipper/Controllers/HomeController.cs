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
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Home
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHomes()
        {
          if (_context.Homes == null)
          {
              return NotFound();
          }

          var userId = "1";

          var userHomes = await _context.Homes.Where(h  => h.ApplicationUserId == userId).ToListAsync();
          return userHomes;
        }

        // GET: api/Home/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Home>> GetHome(int id)
        {
          if (_context.Homes == null)
          {
              return NotFound();
          }
            var home = await _context.Homes.FindAsync(id);

            if (home == null)
            {
                return NotFound();
            }

            return home;
        }

        // PUT: api/Home/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHome(int id, Home home)
        {
            if (id != home.Id)
            {
                return BadRequest();
            }

            _context.Entry(home).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeExists(id))
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

        // POST: api/Home
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Home>> PostHome(Home home)
        {
          if (_context.Homes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Homes'  is null.");
          }
            _context.Homes.Add(home);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHome", new { id = home.Id }, home);
        }

        // DELETE: api/Home/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            if (_context.Homes == null)
            {
                return NotFound();
            }
            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }

            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HomeExists(int id)
        {
            return (_context.Homes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
