using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersDiscountsController : ControllerBase
    {
        private readonly PraktikaSevContext _context;

        public UsersDiscountsController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/UsersDiscounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDiscount>>> GetUsersDiscounts([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.UsersDiscounts == null)
            {
                return NotFound();
            }

            var pageData = await _context.UsersDiscounts.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<UsersDiscount>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/UsersDiscounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDiscount>> GetUsersDiscount(int? id)
        {
          if (_context.UsersDiscounts == null)
          {
              return NotFound();
          }
            var usersDiscount = await _context.UsersDiscounts.FindAsync(id);

            if (usersDiscount == null)
            {
                return NotFound();
            }

            return usersDiscount;
        }

        // PUT: api/UsersDiscounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersDiscount(int? id, UsersDiscount usersDiscount)
        {
            if (id != usersDiscount.IdUserDiscount)
            {
                return BadRequest();
            }

            _context.Entry(usersDiscount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersDiscountExists(id))
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

        // POST: api/UsersDiscounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersDiscount>> PostUsersDiscount(UsersDiscount usersDiscount)
        {
          if (_context.UsersDiscounts == null)
          {
              return Problem("Entity set 'PraktikaSevContext.UsersDiscounts'  is null.");
          }
            _context.UsersDiscounts.Add(usersDiscount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersDiscount", new { id = usersDiscount.IdUserDiscount }, usersDiscount);
        }

        // DELETE: api/UsersDiscounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersDiscount(int? id)
        {
            if (_context.UsersDiscounts == null)
            {
                return NotFound();
            }
            var usersDiscount = await _context.UsersDiscounts.FindAsync(id);
            if (usersDiscount == null)
            {
                return NotFound();
            }

            _context.UsersDiscounts.Remove(usersDiscount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersDiscountExists(int? id)
        {
            return (_context.UsersDiscounts?.Any(e => e.IdUserDiscount == id)).GetValueOrDefault();
        }
    }
}
