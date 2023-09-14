using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        HashAlgorithm hashAlg = new HashAlgorithm();
        private readonly PraktikaSevContext _context;

        public UsersController(PraktikaSevContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (_context.Users == null)
            {
                return NotFound();
            }

            var pageData = await _context.Users.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                  .Take(validFilter.PageSize).ToListAsync();
            return Ok(new PageResponse<List<User>>(pageData, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpPut("Login")]
        public async Task<ActionResult<IEnumerable<User>>> Authorization(string login, string password)
        {
            if(_context.Users == null)
            {
                return NotFound();
            }
            foreach(var user in _context.Users)
            {
                if(user.UserLogin == login.Trim())
                {
                    var b = hashAlg.AreEqual(password, user.UserPassword, user.Salt);
                    if(b == true)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Неправильные данные для входа!");
                    }
                }
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int? id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'PraktikaSevContext.Users'  is null.");
          }
            user.IdUser = null;
            user.Salt = hashAlg.CreateSalt(8);
            user.UserPassword = hashAlg.GenerateHash(user.UserPassword, user.Salt);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int? id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
