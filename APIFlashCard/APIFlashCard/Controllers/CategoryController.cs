using APIFlashCard.Data;
using APIFlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
                return BadRequest(new { message = "Nieprawidłowe dane kategorii." });
            }

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(AddCategory), new { id = category.ID_Category }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Wystąpił błąd: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}