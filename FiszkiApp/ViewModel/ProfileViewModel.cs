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

namespace FiszkiApp.ViewModel
{
    public partial class ProfileViewModel : MainViewModel
    {

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

            if (isAny == null || string.IsNullOrEmpty(isAny.Name))
            {
                var result = countriesWithUrl.Find(x => x.Country == value);
                AddItem(result.Country, result.Url);
                
            }
            CountryPicked = null;
        }

        public ProfileViewModel(AuthService authService)
        {
            _authService = authService;
            LoadCountriesCommand = new AsyncRelayCommand(LoadCountry);
            LoadCountriesCommand.Execute(null);
            LoadCountriesUrlCommand = new AsyncRelayCommand(LoadCountryUrl);
            LoadCountriesUrlCommand.Execute(null);
            DeleteCommand = new Command<object>(OnTapped);
        }

        private void OnTapped(object obj)
        {
            var item = obj as Item;
            Items.Remove(item);            
        }

        public async Task OnNavigatedTo(NavigationEventArgs args)
        {
            try
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
                Country = "Kraj pochodzenia: " + country;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas dodawania obrazka: {ex.Message}");
            }
        }


        private async Task LoadCountry()
        {
            var countriesDic = new dbConnetcion.SQLQueries.CountriesDic();
            var countries = await countriesDic.Countries();
            CountryPicker = new ObservableCollection<string>(countries);
        }
        private async Task LoadCountryUrl()
        {
            var countriesUrl = new dbConnetcion.SQLQueries.CountriesUrl();
            countriesWithUrl = await countriesUrl.CountriesU();
        }


        private readonly AuthService _authService;

        [RelayCommand]
        public async void LogoutCommand(AuthService authService)
        {
            _authService.Logout();
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
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
