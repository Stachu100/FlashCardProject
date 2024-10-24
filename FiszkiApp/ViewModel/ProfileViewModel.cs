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
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using FiszkiApp.EntityClasses;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CommunityToolkit.Maui.ImageSources;

namespace FiszkiApp.ViewModel
{
    public partial class ProfileViewModel : MainViewModel
    {
        private readonly AuthService _authService;

        public IAsyncRelayCommand LoadCountriesCommand { get; }
        public IAsyncRelayCommand LoadCountriesUrlCommand { get; }
        public Command<object> DeleteCommand { get; set; }

        private List<(string Country, string Url)> countriesWithUrl;
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        [ObservableProperty]

        private byte[] imageAsBytes;

        [ObservableProperty]
        private string user;

        [ObservableProperty]
        private string country;

        [ObservableProperty]
        private ObservableCollection<string> countryPicker;

        [ObservableProperty]
        private string countryPicked;
        partial void OnCountryPickedChanged(string value)
        {
            var isAny = Items.FirstOrDefault(x => x.Name == value);

            if (isAny == null)
            {
                var result = countriesWithUrl.FirstOrDefault(x => x.Country == value);
                if (!string.IsNullOrEmpty(result.Country))
                {
                    AddItem(result.Country, result.Url);
                }
            }
            CountryPicked = null;
        }

        public ProfileViewModel(AuthService authService)
        {
            _authService = authService;
            LoadCountriesCommand = new AsyncRelayCommand(LoadCountry);
            LoadCountriesCommand.Execute(null);
            DeleteCommand = new Command<object>(OnTapped);
        }

        private void OnTapped(object obj)
        {
            if (obj is Item item)
            {
                Items.Remove(item);
            }
        }

        public async Task OnNavigatedTo(NavigationEventArgs args)
        {
            try
            {
                var (isAuthenticated, userID) = await _authService.IsAuthenticatedAsync();
                int intUserId = Convert.ToInt32(userID);
                var profileDetails = new dbConnetcion.SQLQueries.ProfileDetails();

                var userDetails = await profileDetails.GetUserDetailsAsync(intUserId);

                if (userDetails != null)
                {
                    User = $"{userDetails.FirstName} {userDetails.LastName}";
                    Country = "Kraj pochodzenia: " + userDetails.Country;

                    if (userDetails.Avatar != null && userDetails.Avatar.Length > 0)
                    {
                        ImageAsBytes = userDetails.Avatar;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas pobierania szczegółów użytkownika: {ex.Message}");
            }
        }

        private async Task LoadCountry()
        {
            var countriesDic = new dbConnetcion.SQLQueries.CountriesDic();
            var countries = await countriesDic.GetCountriesWithFlagsAsync();

            countriesWithUrl = countries.Select(c => (c.Country, c.Url)).ToList();
            CountryPicker = new ObservableCollection<string>(countries.Select(c => c.Country));
        }

        [RelayCommand]
        public async Task LogoutCommand(AuthService authService)
        {
            _authService.Logout();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private void AddItem(string name, string imageName)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ImageSource imgSource = ImageSource.FromFile(imageName);
                var item = new EntityClasses.Item { Name = name, Image = imgSource };    
                
                Items.Add(item);
                
            }
        }
    }
}
