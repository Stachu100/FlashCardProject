using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using FiszkiApp.EntityClasses;

public class CategoryPost
{
    private readonly HttpClient _httpClient;

    public CategoryPost()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7190/api/");
    }

    public async Task<bool> AddCategoryAsync(Category category)
    {
        try
        {
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("category", content);

            // Loguj odpowiedź
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Status Code: {response.StatusCode}, Content: {responseContent}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                // Logowanie błędu lub dodatkowa obsługa
                return false;
            }
        }
        catch (Exception ex)
        {
            // Obsługa błędów
            Console.WriteLine($"Exception: {ex.Message}");
            return false;
        }
    }
}
