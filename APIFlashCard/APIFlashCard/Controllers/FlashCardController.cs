using APIFlashCard.Data;
using APIFlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace APIFlashCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashCardController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public FlashCardController(FlashCardDbContext context)
        {
            _context = context;
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddFlashCards([FromBody] List<FlashCard> flashCards)
        {
            if (flashCards == null || !flashCards.Any())
            {
                return BadRequest(new { message = "Lista fiszek jest pusta lub nieprawidłowa." });
            }

            try
            {
                _context.FlashCards.AddRange(flashCards);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Fiszki zostały dodane pomyślnie.", count = flashCards.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Wystąpił błąd: {ex.Message}" });
            }
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<FlashCard>>> GetFlashCardsByCategory(int categoryId)
        {
            try
            {
                var flashcards = await _context.FlashCards
                    .Where(f => f.ID_Category == categoryId)
                    .Select(f => new { f.FrontFlashCard, f.BackFlashCard })
                    .ToListAsync();

                return Ok(flashcards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Wystąpił błąd: {ex.Message}" });
            }
        }

        [HttpGet("display/{categoryId}/{page}")]
        public async Task<ActionResult<IEnumerable<FlashCard>>> GetFlashCardsForDisplay(int categoryId, int page = 1)
        {
            try
            {
                const int pageSize = 10;

                var flashcards = await _context.FlashCards
                    .Where(f => f.ID_Category == categoryId)
                    .OrderBy(f => f.ID_flashcard)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(flashcards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Wystąpił błąd: {ex.Message}" });
            }
        }
    }
}