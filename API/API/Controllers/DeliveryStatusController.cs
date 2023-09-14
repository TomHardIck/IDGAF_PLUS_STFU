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
    public class DeliveryStatusController : ControllerBase
    {
        private readonly PraktikaSevContext _context;

        public DeliveryStatusController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryStatus>>> GetDeliveryStatuses([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.DeliveryStatuses == null)
            {
                return NotFound();
            }

            var pageData = await _context.DeliveryStatuses.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<DeliveryStatus>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        // GET: api/DeliveryStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryStatus>> GetDeliveryStatus(int? id)
        {
          if (_context.DeliveryStatuses == null)
          {
              return NotFound();
          }
            var deliveryStatus = await _context.DeliveryStatuses.FindAsync(id);

            if (deliveryStatus == null)
            {
                return NotFound();
            }

            return deliveryStatus;
        }

        // PUT: api/DeliveryStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryStatus(int? id, DeliveryStatus deliveryStatus)
        {
            if (id != deliveryStatus.IdDeliveryStatus)
            {
                return BadRequest();
            }

            _context.Entry(deliveryStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryStatusExists(id))
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

        // POST: api/DeliveryStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryStatus>> PostDeliveryStatus(DeliveryStatus deliveryStatus)
        {
          if (_context.DeliveryStatuses == null)
          {
              return Problem("Entity set 'PraktikaSevContext.DeliveryStatuses'  is null.");
          }
            _context.DeliveryStatuses.Add(deliveryStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryStatus", new { id = deliveryStatus.IdDeliveryStatus }, deliveryStatus);
        }

        // DELETE: api/DeliveryStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryStatus(int? id)
        {
            if (_context.DeliveryStatuses == null)
            {
                return NotFound();
            }
            var deliveryStatus = await _context.DeliveryStatuses.FindAsync(id);
            if (deliveryStatus == null)
            {
                return NotFound();
            }

            _context.DeliveryStatuses.Remove(deliveryStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryStatusExists(int? id)
        {
            return (_context.DeliveryStatuses?.Any(e => e.IdDeliveryStatus == id)).GetValueOrDefault();
        }
    }
}
