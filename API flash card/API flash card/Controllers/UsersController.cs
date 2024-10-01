using API_flash_card.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_flash_card.Data;

namespace API_flash_card.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public UserController(FlashCardDbContext context)
        {
            _context = context;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(); // Zwraca 404, jeśli użytkownik nie został znaleziony
            }

            return user; // Zwraca użytkownika, jeśli został znaleziony
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID_User }, user);
        }
    }
}
