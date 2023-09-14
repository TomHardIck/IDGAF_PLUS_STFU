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
    public class DeliveryTypesController : ControllerBase
    {
        private readonly PraktikaSevContext _context;

        public DeliveryTypesController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryType>>> GetDeliveryTypes([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var pageData = await _context.DeliveryTypes.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<DeliveryType>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/DeliveryTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryType>> GetDeliveryType(int? id)
        {
          if (_context.DeliveryTypes == null)
          {
              return NotFound();
          }
            var deliveryType = await _context.DeliveryTypes.FindAsync(id);

            if (deliveryType == null)
            {
                return NotFound();
            }

            return deliveryType;
        }

        // PUT: api/DeliveryTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryType(int? id, DeliveryType deliveryType)
        {
            if (id != deliveryType.IdDeliveryType)
            {
                return BadRequest();
            }

            _context.Entry(deliveryType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeExists(id))
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

        // POST: api/DeliveryTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryType>> PostDeliveryType(DeliveryType deliveryType)
        {
          if (_context.DeliveryTypes == null)
          {
              return Problem("Entity set 'PraktikaSevContext.DeliveryTypes'  is null.");
          }
            _context.DeliveryTypes.Add(deliveryType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryType", new { id = deliveryType.IdDeliveryType }, deliveryType);
        }

        // DELETE: api/DeliveryTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryType(int? id)
        {
            if (_context.DeliveryTypes == null)
            {
                return NotFound();
            }
            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            _context.DeliveryTypes.Remove(deliveryType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryTypeExists(int? id)
        {
            return (_context.DeliveryTypes?.Any(e => e.IdDeliveryType == id)).GetValueOrDefault();
        }
    }
}
