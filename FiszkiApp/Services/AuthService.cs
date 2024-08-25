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
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
            return authState;
        }
        public async void Login()
        {
            Preferences.Default.Set<bool>(AuthStateKey, true);
        }
        public async void Logout()
        {
            Preferences.Default.Remove(AuthStateKey);
        }
    }
}
