using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FiszkiApp.EntityClasses.Models;
using FiszkiApp.EntityClasses;
using static FiszkiApp.EntityClasses.AesManaged;

namespace FiszkiApp.dbConnetcion.SQLQueries
{
    internal class CreateUser
    {
        private readonly HttpClient _httpClient;

        public CreateUser()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5278/api/")
            };
        }

        public async Task<string> UserInsertAsync(UserRegistration userRegistration)
        {
            try
            {
                var emailCheckResponse = await _httpClient.GetAsync($"userdetails/check-email/{userRegistration.Email}");
                if (emailCheckResponse.IsSuccessStatusCode && (await emailCheckResponse.Content.ReadAsStringAsync()) == "exists")
                {
                    return "Istnieje już użytkownik o podanym emailu";
                }

                var usernameCheckResponse = await _httpClient.GetAsync($"user/check-username/{userRegistration.Name}");
                if (usernameCheckResponse.IsSuccessStatusCode && (await usernameCheckResponse.Content.ReadAsStringAsync()) == "exists")
                {
                    return "Istnieje już użytkownik o podanej nazwie";
                }

                var user = new User { UserName = userRegistration.Name, UserPassword = userRegistration.EncryptedPassword };
                var userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var userResponse = await _httpClient.PostAsync("user", userContent);

                if (!userResponse.IsSuccessStatusCode)
                {
                    return "Wystąpił błąd podczas rejestracji użytkownika";
                }

                var createdUser = JsonConvert.DeserializeObject<User>(await userResponse.Content.ReadAsStringAsync());
                int userId = createdUser.ID_User;

                var userDetails = new UserDetails
                {
                    ID_User = userId,
                    FirstName = userRegistration.FirstName,
                    LastName = userRegistration.LastName,
                    Country = userRegistration.Country,
                    Email = userRegistration.Email,
                    Avatar = userRegistration.UploadedImage,
                    IsAcceptedPolicy = userRegistration.IsAcceptedPolicy
                };
                var detailsContent = new StringContent(JsonConvert.SerializeObject(userDetails), Encoding.UTF8, "application/json");
                var detailsResponse = await _httpClient.PostAsync("userdetails", detailsContent);

                if (!detailsResponse.IsSuccessStatusCode)
                {
                    return "Wystąpił błąd podczas rejestracji szczegółów użytkownika";
                }

                var encryptionResult = AesManaged.Encryption(userRegistration.Password);

                var encryptionKeys = new EncryptionKeys
                {
                    ID_User = userId,
                    EncryptionKey = encryptionResult.EncryptedData,
                    IV = encryptionResult.IV
                };

                var keysContent = new StringContent(JsonConvert.SerializeObject(encryptionKeys), Encoding.UTF8, "application/json");
                var keysResponse = await _httpClient.PostAsync("encryptionkeys", keysContent);

                if (!keysResponse.IsSuccessStatusCode)
                {
                    return "Wystąpił błąd podczas dodawania kluczy szyfrowania";
                }

                return "Rejestracja zakończyła się sukcesem";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return "Wystąpił błąd podczas rejestracji";
            }
        }
    }
}
