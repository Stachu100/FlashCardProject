using APIFlashCard.Data;
using APIFlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APIFlashCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public UserDetailsController(FlashCardDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            try
            {
                var userDetails = await _context.UserDetails
                    .Where(u => u.ID_User == userId)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Country,
                        u.Avatar
                    })
                    .FirstOrDefaultAsync();

                if (userDetails == null)
                {
                    return NotFound(new { message = "Nie znaleziono użytkownika." });
                }

                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Wystąpił błąd: {ex.Message}" });
            }
        }
    }
}
