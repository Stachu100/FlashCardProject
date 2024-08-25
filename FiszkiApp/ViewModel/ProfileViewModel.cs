using CommunityToolkit.Mvvm.Input;
using FiszkiApp.Services;
using FiszkiApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.ViewModel
{
    public partial class ProfileViewModel : MainViewModel
    {
        private readonly AuthService _authService;

        public ProfileViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        public async void LogoutCommand(AuthService authService)
        {
            _authService.Logout();
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
