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
                return CreatedAtAction(nameof(AddCategory), new { id = category.ID_Category }, new { message = "Kategoria została dodana pomyślnie." });
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

        // Budowanie listy
        [HttpPost("search")]
        public async Task<IActionResult> SearchCategories([FromBody] dynamic filters)
        {
            var query = _context.Categories.AsQueryable();

            try
            {
                JsonElement filtersElement = (JsonElement)filters;

                if (filtersElement.TryGetProperty("CategoryName", out var categoryNameProperty) &&
                    categoryNameProperty.ValueKind != JsonValueKind.Null)
                {
                    string categoryName = categoryNameProperty.GetString();
                    query = query.Where(c => EF.Functions.Like(c.CategoryName, $"%{categoryName}%"));
                }

                if (filtersElement.TryGetProperty("UserName", out var userNameProperty) &&
                    userNameProperty.ValueKind != JsonValueKind.Null)
                {
                    string userName = userNameProperty.GetString().ToLower();
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName);
                    if (user != null)
                    {
                        query = query.Where(c => c.UserID == user.ID_User);
                    }
                    else
                    {
                        return Ok(new List<Category>());
                    }
                }

                if (filtersElement.TryGetProperty("LanguageLevel", out var languageLevelProperty) &&
                    languageLevelProperty.ValueKind != JsonValueKind.Null)
                {
                    string languageLevel = languageLevelProperty.GetString();
                    query = query.Where(c => c.LanguageLevel == languageLevel);
                }

                if (filtersElement.TryGetProperty("UserLanguage", out var userLanguageProperty) &&
                    userLanguageProperty.ValueKind != JsonValueKind.Null)
                {
                    string userLanguage = userLanguageProperty.GetString();
                    query = query.Where(c =>
                        c.FrontLanguage == userLanguage ||
                        c.BackLanguage == userLanguage
                    );
                }

                var categories = await query.ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }
        }
    }
}