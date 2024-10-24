using System;
using System.Net.Http;
using System.Threading.Tasks;
using FiszkiApp.EntityClasses;
using Newtonsoft.Json;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    public class ProfileDetails
    {
        private readonly HttpClient _httpClient;

        public ProfileDetails()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5278/api/")
            };
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
