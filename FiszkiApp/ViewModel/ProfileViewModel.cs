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

namespace FiszkiApp.ViewModel
{
    public partial class ProfileViewModel : MainViewModel
    {

        public IAsyncRelayCommand LoadCountriesCommand { get; }
        public ObservableCollection<ImageSource> Items { get; } = new ObservableCollection<ImageSource>();
        //public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

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
                AddItem(value, "Flags/flag_poland.jpg");
                CountryPicked = null;            
        }

        public ProfileViewModel(AuthService authService)
        {
            _authService = authService;
            LoadCountriesCommand = new AsyncRelayCommand(LoadCountry);
            LoadCountriesCommand.Execute(null);
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
            Country = "Kraj pochodzenia: " + country;

        }

        private async Task LoadCountry()
        {
            var countriesDic = new dbConnetcion.SQLQueries.CountriesDic();
            var countries = await countriesDic.Countries();
            CountryPicker = new ObservableCollection<string>(countries);
        }

        private readonly AuthService _authService;

        [RelayCommand]
        public async void LogoutCommand(AuthService authService)
        {
            _authService.Logout();
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        void AddItem(string strValue, string imagePath)
        {
            if (!string.IsNullOrEmpty(strValue))
            {

                ImageSource imgSource = ImageSource.FromFile(imagePath);


                if (!Items.Contains((imgSource)))
                // if (!Items.Any(item => item.Text == strValue && item.Image == imgSource))
                {
                    Items.Add((imgSource));
                    //Items.Add(new Item { Text = strValue, Image = imgSource });
                    CountryPicked = null;
                }
            }
        }
        //private void AddItem(string name, string imageName)
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        ImageSource imgSource = ImageSource.FromFile(imageName);
        //        var item = new EntityClasses.Item { Name = name, Image = imgSource };

        //        // Sprawdź, czy element już istnieje, zanim go dodasz
        //        if (!Items.Any(i => i.Name == name && i.Image == imgSource))
        //        {
        //            Items.Add(item);
        //        }
        //    }
        //}
    }
}
