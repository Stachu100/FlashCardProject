using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_flash_card.Data;
using API_flash_card.Models;

namespace API_flash_card.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly FlashCardDbContext _context;

        public CountriesController(FlashCardDbContext context)
        {
            _context = context;
        }

        // GET: api/countries/polska
        [HttpGet("{countryName}")]
        public async Task<ActionResult<Countries>> GetCountryByName(string countryName)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Country.ToLower() == countryName.ToLower());

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }
    }
}
