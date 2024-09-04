using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp.Services;
using FiszkiApp.View;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.ViewModel
{
    public partial class ProfileViewModel : MainViewModel
    {
        public ProfileViewModel(AuthService authService)
        {
            _authService = authService;            
        }

        public async Task OnNavigatedTo(NavigationEventArgs args)
        {
            var (isAuthenticated, userID) = await _authService.IsAuthenticatedAsync();
            int IntUserId = Convert.ToInt32(userID);
            var profileDetails = new dbConnetcion.SQLQueries.ProfiileDetails();
            var (uploadedImage, user, country) = await profileDetails.UserDetails(IntUserId);

            if (uploadedImage.Length > 0)
            {
                ImageAsBytes = uploadedImage;
            }
            User = user;
            Country = country;

        }

        [ObservableProperty]
        private byte[] imageAsBytes;

        [ObservableProperty]
        private string user;

        [ObservableProperty]
        private string country;

        

        private readonly AuthService _authService;

        [RelayCommand]
        public async void LogoutCommand(AuthService authService)
        {
            _authService.Logout();
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
