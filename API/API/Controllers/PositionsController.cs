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
    public class PositionsController : ControllerBase
    {
        private readonly PraktikaSevContext _context;

        public PositionsController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.Positions == null)
            {
                return NotFound();
            }

            var pageData = await _context.Positions.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<Position>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int? id)
        {
          if (_context.Positions == null)
          {
              return NotFound();
          }
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int? id, Position position)
        {
            if (id != position.IdPosition)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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

        [HttpPut("DeactivateMany")]
        public async Task<IActionResult> DeactivateMany(int[] ids)
        {
            if(_context.Positions == null)
            {
                return NotFound();
            }
            foreach(int i in ids)
            {
                var position = await _context.Positions.FindAsync(i);
                if(position == null)
                {
                    return NotFound();
                }

                position.IsExists = false;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("DeactivateOne")]
        public async Task<IActionResult> DeactivateOne(int id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            position.IsExists = false;
            await _context.SaveChangesAsync();
            return Ok(position);
        }

        [HttpPut("ActivateMany")]
        public async Task<IActionResult> ActivateMany(int[] ids)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            foreach (int i in ids)
            {
                var position = await _context.Positions.FindAsync(i);
                if (position == null)
                {
                    return NotFound();
                }

                position.IsExists = true;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("ActivateOne")]
        public async Task<IActionResult> ActivateOne(int id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            position.IsExists = true;
            await _context.SaveChangesAsync();
            return Ok(position);
        }

        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
          if (_context.Positions == null)
          {
              return Problem("Entity set 'PraktikaSevContext.Positions'  is null.");
          }
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.IdPosition }, position);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int? id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(int? id)
        {
            return (_context.Positions?.Any(e => e.IdPosition == id)).GetValueOrDefault();
        }
    }
}
