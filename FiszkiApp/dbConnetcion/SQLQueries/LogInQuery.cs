using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FiszkiApp.EntityClasses.Models;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    internal class LogInQuery
    {
        private readonly HttpClient _httpClient;

        public LogInQuery()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5278/api/")
            };
        }

        public async Task<string> UserLogIn(string name, string password)
        {
            try
            {
                var userResponse = await _httpClient.GetAsync($"user/{name}");
                if (!userResponse.IsSuccessStatusCode)
                {
                    return "Hasło lub login jest nie poprawne";
                }

                var userJson = await userResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(userJson);

                if (user == null)
                {
                    return "Hasło lub login jest nie poprawne";
                }

                var keysResponse = await _httpClient.GetAsync($"encryptionkeys/{user.ID_User}");
                if (!keysResponse.IsSuccessStatusCode)
                {
                    return "Wystąpił problem z pobraniem kluczy szyfrowania.";
                }

                var keysJson = await keysResponse.Content.ReadAsStringAsync();
                var encryptionKeys = JsonConvert.DeserializeObject<EncryptionKeys>(keysJson);

                if (encryptionKeys == null)
                {
                    return "Wystąpił problem z pobraniem kluczy szyfrowania.";
                }

                string decryptedPassword = EntityClasses.AesManaged.Decryption(user.UserPassword, encryptionKeys.EncryptionKey, encryptionKeys.IV);

                if (decryptedPassword != null && decryptedPassword == password)
                {
                    return Convert.ToString(user.ID_User);
                }
                else
                {
                    return "Hasło lub login jest nie poprawne";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas logowania: {ex.Message}");
                return "Wystąpił błąd podczas logowania";
            }
        }
    }
}
