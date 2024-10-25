using APIFlashCard.Data;
using APIFlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APIFlashCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionKeysController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public EncryptionKeysController(FlashCardDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<EncryptionKeys>> GetEncryptionKeys(int userId)
        {
            var encryptionKeys = await _context.EncryptionKeys
                .Where(k => k.ID_User == userId)
                .Select(k => new EncryptionKeys
                {
                    ID_User = k.ID_User,
                    EncryptionKey = k.EncryptionKey,
                    IV = k.IV
                })
                .FirstOrDefaultAsync();

            if (encryptionKeys == null)
            {
                return NotFound("Klucze szyfrowania dla użytkownika nie zostały znalezione.");
            }

            return encryptionKeys;
        }
    }
}

