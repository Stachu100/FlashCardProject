using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiszkiApp;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FiszkiApp.ViewModel
{
    public partial class RegisterViewModel : MainViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _userFirstName;

        [ObservableProperty]
        private string _userLastName;

        [ObservableProperty]
        private string _userCountry;

        [ObservableProperty]
        private string _userPassword;

        [ObservableProperty]
        private string _userRepeatPassword;

        [ObservableProperty]
        private string _userEmail;

        private readonly Dictionary<string, string> _userDetails;

        public RegisterViewModel()
        {
            _userDetails = new Dictionary<string, string>
            {
                { nameof(UserName), _userName },
                { nameof(UserFirstName), _userFirstName },
                { nameof(UserLastName), _userLastName },
                { nameof(UserCountry), _userCountry },
                { nameof(UserPassword), _userPassword },
                { nameof(UserRepeatPassword), _userRepeatPassword },
                { nameof(UserEmail), _userEmail }
            };
        }
        string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        [RelayCommand]
        public async void Register()
        {
            if (AnyPropertyIsNullOrEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Wypełnij wszystkie pola", "OK");
            }

            if (!Regex.IsMatch(UserEmail, pattern))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Nie poprawny email", "OK");
            }



            
        }

        private bool AnyPropertyIsNullOrEmpty()
        {

            foreach (var entry in _userDetails)
            {
                if (string.IsNullOrEmpty(entry.Value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
