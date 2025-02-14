using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CafeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cafe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cafe>>> GetCafes([FromQuery] string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return await _context.Cafes
                    .Include(c => c.Employees)
                    .OrderByDescending(c => c.Employees.Count)
                    .ToListAsync();
            }

            var cafes = await _context.Cafes
                .Include(c => c.Employees)
                .Where(c => c.Location == location)
                .OrderByDescending(c => c.Employees.Count)
                .ToListAsync();

            if (cafes == null || cafes.Count == 0)
            {
                return new List<Cafe>();
            }

            return cafes;
        }

        // POST: api/Cafe
        [HttpPost]
        public async Task<ActionResult<Cafe>> PostCafe(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCafe", new { id = cafe.Id }, cafe);
        }

        // PUT: api/Cafe/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCafe(Guid id, Cafe cafe)
        {
            if (id != cafe.Id)
            {
                return BadRequest();
            }

            _context.Entry(cafe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CafeExists(id))
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

        // DELETE: api/Cafe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            var cafe = await _context.Cafes
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cafe == null)
            {
                return NotFound();
            }

            _context.Employees.RemoveRange(cafe.Employees);
            _context.Cafes.Remove(cafe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CafeExists(Guid id)
        {
            return _context.Cafes.Any(e => e.Id == id);
        }
    }
}
