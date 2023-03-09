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
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Home/user/f9fn329sp
        // GET all Homes associated with User
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Home>>> GetHomes(string userId)
        {
          if (_context.Homes == null)
          {
              return NotFound();
          }

          var userHomes = await _context.Homes.Where(h  => h.ApplicationUserId == userId).ToListAsync();
          return userHomes;
        }

        // GET: api/Home/5
        // GET Home by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeGet>> GetHome(int id)
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

            var homeGet = HomeConverter.HomeToHomeGet(home);
            
            return homeGet;
        }

        // PUT: api/Home
        [HttpPut]
        public async Task<IActionResult> PutHome(HomePut homePut)
        {
            if (homePut == null)
            {
                return BadRequest();
            }

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

            var home = new Home()
            {
                Id = homePut.Id,
                ApplicationUserId = homePut.ApplicationUserId,
                Date = DateTime.Now.ToString(),
                Name = homePut.Name
            };

            _context.Entry(home).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeExists(home.Id))
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
        public async Task<IActionResult> PostHome(HomePost home)
        {
          if (home == null)
          {
              return NotFound();
          }

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          var homeCreate = new Home()
          {
              ApplicationUserId = home.ApplicationUserId,
              Name = home.Name,
              Date = DateTime.Now.ToString(),
          };
          
            _context.Homes.Add(homeCreate);
            await _context.SaveChangesAsync();

            
            var tempHome = new Home()
            {
                ApplicationUserId = homeCreate.ApplicationUserId,
                Name = homeCreate.Name,
                Date = homeCreate.Date,
            };

            return Ok(tempHome);
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
