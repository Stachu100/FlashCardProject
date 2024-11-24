using APIFlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIFlashCard.Data;

namespace APIFlashCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCountriesController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public UserCountriesController(FlashCardDbContext context)
        {
            _context = context;
        }

        // GET: api/UserCountries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCountries>>> GetUserCountries()
        {
            return await _context.UserCountries.ToListAsync();
        }

        // GET: api/UserCountries/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCountries>> GetUserCountry(int id)
        {
            var userCountry = await _context.UserCountries.FindAsync(id);

            if (userCountry == null)
            {
                return NotFound();
            }

            return userCountry;
        }

        // POST: api/UserCountries
        [HttpPost]
        public async Task<IActionResult> PostUserCountry(UserCountries userCountry)
        {
            if (userCountry == null)
            {
                return BadRequest("Nieprawidłowe dane");
            }

            _context.UserCountries.Add(userCountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserCountry), new { id = userCountry.ID_UserCountries }, userCountry);
        }

        // DELETE: api/UserCountries/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCountry(int id)
        {
            var userCountry = await _context.UserCountries.FindAsync(id);

            if (userCountry == null)
            {
                return NotFound();
            }

            _context.UserCountries.Remove(userCountry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
