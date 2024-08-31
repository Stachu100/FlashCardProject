using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.Services
{
    public class AuthService
    {
        private const string AuthStateKey = "AuthState";
        private const string UserNameKey = "UserName";
        private const string UserPasswordKey = "UserPassword";
        private const string UserId = "UserId";
        public async Task<(bool AuthStateKey, string UserId)> IsAuthenticatedAsync()
        {

            string result = "";
            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
            var userName = Preferences.Default.Get<string>(UserNameKey, null);
            var userPassword = Preferences.Default.Get<string>(UserPasswordKey, null);
            var userId = Preferences.Default.Get<string>(UserPasswordKey, null);

            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(userPassword) )
            {
                authState = false;
            }
            else
            {
                var loginInQuery = new dbConnetcion.SQLQueries.LogInQuery();
                result = await loginInQuery.UserLogIn(userName, userPassword);
            }
            return (authState, result);
        }
        public async Task<string> Login(string UserName, string UserPassword)
        {
            var loginInQuery = new dbConnetcion.SQLQueries.LogInQuery();
            string result = await loginInQuery.UserLogIn(UserName, UserPassword);
            if (result != "Hasło lub login jest nie poprawne" && result != "Wystąpił błąd podczas logowania")
            {
                Preferences.Default.Set<bool>(AuthStateKey, true);
                Preferences.Default.Set<string>(UserNameKey, UserName);
                Preferences.Default.Set<string>(UserPasswordKey, UserPassword);
                Preferences.Default.Set<string>(UserPasswordKey, result);

            }
            return result;
            
        }
        public async void Logout()
        {
            Preferences.Default.Remove(AuthStateKey);
        }
    }
}
