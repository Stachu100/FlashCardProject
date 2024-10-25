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
                var userResponse = await _httpClient.GetAsync($"user?username={name}");
                if (!userResponse.IsSuccessStatusCode)
                {
                    return "Hasło lub login jest niepoprawne";
                }

                var user = JsonConvert.DeserializeObject<User>(await userResponse.Content.ReadAsStringAsync());
                int userId = user.ID_User;

                var encryptionResponse = await _httpClient.GetAsync($"encryptionkeys/{userId}");
                if (!encryptionResponse.IsSuccessStatusCode)
                {
                    return "Hasło lub login jest niepoprawne";
                }

                var encryptionKeys = JsonConvert.DeserializeObject<EncryptionKeys>(await encryptionResponse.Content.ReadAsStringAsync());

                var passwordToDecrypt = encryptionKeys.EncryptedPassword;
                var encryptionKey = encryptionKeys.EncryptionKey;
                var iv = encryptionKeys.IV;

                string decryptedPassword = EntityClasses.AesManaged.Decryption(passwordToDecrypt, encryptionKey, iv);
                if (decryptedPassword != null && decryptedPassword == password)
                {
                    return Convert.ToString(userId);
                }
                else
                {
                    return "Hasło lub login jest niepoprawne";
                }
            }
            catch (Exception ex)
            {
                return "Wystąpił błąd podczas logowania: " + ex.Message;
            }
        }
    }
}
