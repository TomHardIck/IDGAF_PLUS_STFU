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
    public class PositionsInOrdersController : ControllerBase
    {
        private readonly PraktikaSevContext _context;

        public PositionsInOrdersController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/PositionsInOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionsInOrder>>> GetPositionsInOrders([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.PositionsInOrders == null)
            {
                return NotFound();
            }

            var pageData = await _context.PositionsInOrders.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<PositionsInOrder>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/PositionsInOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionsInOrder>> GetPositionsInOrder(int? id)
        {
          if (_context.PositionsInOrders == null)
          {
              return NotFound();
          }
            var positionsInOrder = await _context.PositionsInOrders.FindAsync(id);

            if (positionsInOrder == null)
            {
                return NotFound();
            }

            return positionsInOrder;
        }

        // PUT: api/PositionsInOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionsInOrder(int? id, PositionsInOrder positionsInOrder)
        {
            if (id != positionsInOrder.IdPositionInOrder)
            {
                return BadRequest();
            }

            _context.Entry(positionsInOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionsInOrderExists(id))
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

        // POST: api/PositionsInOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionsInOrder>> PostPositionsInOrder(PositionsInOrder positionsInOrder)
        {
          if (_context.PositionsInOrders == null)
          {
              return Problem("Entity set 'PraktikaSevContext.PositionsInOrders'  is null.");
          }
            _context.PositionsInOrders.Add(positionsInOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionsInOrder", new { id = positionsInOrder.IdPositionInOrder }, positionsInOrder);
        }

        // DELETE: api/PositionsInOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionsInOrder(int? id)
        {
            if (_context.PositionsInOrders == null)
            {
                return NotFound();
            }
            var positionsInOrder = await _context.PositionsInOrders.FindAsync(id);
            if (positionsInOrder == null)
            {
                return NotFound();
            }

            _context.PositionsInOrders.Remove(positionsInOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionsInOrderExists(int? id)
        {
            return (_context.PositionsInOrders?.Any(e => e.IdPositionInOrder == id)).GetValueOrDefault();
        }
    }
}
