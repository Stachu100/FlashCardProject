using System.Threading.Tasks;
using FiszkiApp.EntityClasses.Models;
using FiszkiApp.Services;
using Newtonsoft.Json;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    public class ProfileDetails
    {
        private readonly HttpClient _httpClient;

        public ProfileDetails()
        {
            _httpClient = HttpClientService.Instance.HttpClient;
        }

        public async Task<UserDetails?> GetUserDetailsAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"userdetails/{userId}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var userDetails = JsonConvert.DeserializeObject<UserDetails>(responseContent);

                return userDetails;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Błąd HTTP: {httpEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return null;
            }
        }
    }
}
