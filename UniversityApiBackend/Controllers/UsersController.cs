using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]   //  la direccion seria https://localhost:7111/api/Users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        public UsersController(UniversityDBContext context)
        {
            _context = context;
        }

        // GET: api/Users https://localhost:7111/api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        { 
              return await _context.Users.ToListAsync(); 
        }

        // GET: api/Users/5    https://localhost:7111/api/Users     y se agrega un / y el id del usuario
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          var user = await _context.Users.FindAsync(id); // FindAsyng(id) quiere decir que busca por ID
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: https://localhost:7111/api/Users/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(); // si no coincide con el ide retorna un 400
            }

            _context.Entry(user).State = EntityState.Modified;   // Debuelve un estado modificado para que se sepa que se modifico

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

            return NoContent(); // se devuelve un 204 de que todo ah ido bien
        }

        // POST: api/Users  https://localhost:7111/api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>>PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UniversityDBContext.User'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user); // se pasa ID y el USUARIO
        }

        // DELETE: api/Users/5    https://localhost:7111/api/Users
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id); // Busca por ID
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(user => user.Id == id)).GetValueOrDefault();
        }
    }
}
