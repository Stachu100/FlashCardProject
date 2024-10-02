using APIFlashCard.Data;
using APIFlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIFlashCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public CategoryController(FlashCardDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Nieprawidłowe dane kategorii.");
            }

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return Ok("Kategoria została dodana pomyślnie.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}
