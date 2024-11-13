using System.Threading.Tasks;
using FiszkiApp.Services;
using FiszkiApp.EntityClasses.Models;
using Newtonsoft.Json;

namespace FiszkiApp.dbConnetcion.APIQueries
{
    public class CountriesDic
    {
        private readonly HttpClient _httpClient;

        public CountriesDic()
        {
            _httpClient = HttpClientService.Instance.HttpClient;
        }

        public async Task<List<Countries>> GetCountriesWithFlagsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("countries");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var countriesWithFlags = JsonConvert.DeserializeObject<List<Countries>>(responseContent);

                return countriesWithFlags;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Error: {httpEx.Message}");
                return new List<Countries>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return new List<Countries>();
            }
        }
    }
}