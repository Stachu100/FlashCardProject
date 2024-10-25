using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using FiszkiApp.EntityClasses.Models;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    public class CategoryPost
    {
        private readonly HttpClient _httpClient;

        public CategoryPost()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5278/api/")
            };
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            try
            {
                var json = JsonConvert.SerializeObject(category);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Category", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Status Code: {response.StatusCode}, Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Błąd: {responseContent}");
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Error: {httpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }
    }
}
