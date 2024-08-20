using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.Services
{
    public class AuthService
    {
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(1000);
            return false;
        }
    }
}
