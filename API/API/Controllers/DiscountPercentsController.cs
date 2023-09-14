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
    public class DiscountPercentsController : ControllerBase
    {
        private readonly PraktikaSevContext _context;

        public DiscountPercentsController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/DiscountPercents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountPercent>>> GetDiscountPercents([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.DiscountPercents == null)
            {
                return NotFound();
            }

            var pageData = await _context.DiscountPercents.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<DiscountPercent>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/DiscountPercents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountPercent>> GetDiscountPercent(int? id)
        {
          if (_context.DiscountPercents == null)
          {
              return NotFound();
          }
            var discountPercent = await _context.DiscountPercents.FindAsync(id);

            if (discountPercent == null)
            {
                return NotFound();
            }

            return discountPercent;
        }

        // PUT: api/DiscountPercents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscountPercent(int? id, DiscountPercent discountPercent)
        {
            if (id != discountPercent.IdDiscountPercent)
            {
                return BadRequest();
            }

            _context.Entry(discountPercent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountPercentExists(id))
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

        // POST: api/DiscountPercents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiscountPercent>> PostDiscountPercent(DiscountPercent discountPercent)
        {
          if (_context.DiscountPercents == null)
          {
              return Problem("Entity set 'PraktikaSevContext.DiscountPercents'  is null.");
          }
            _context.DiscountPercents.Add(discountPercent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiscountPercent", new { id = discountPercent.IdDiscountPercent }, discountPercent);
        }

        // DELETE: api/DiscountPercents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountPercent(int? id)
        {
            if (_context.DiscountPercents == null)
            {
                return NotFound();
            }
            var discountPercent = await _context.DiscountPercents.FindAsync(id);
            if (discountPercent == null)
            {
                return NotFound();
            }

            _context.DiscountPercents.Remove(discountPercent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscountPercentExists(int? id)
        {
            return (_context.DiscountPercents?.Any(e => e.IdDiscountPercent == id)).GetValueOrDefault();
        }
    }
}
