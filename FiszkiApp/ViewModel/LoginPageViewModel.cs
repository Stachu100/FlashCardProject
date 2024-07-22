using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel.DataAnnotations;
namespace FiszkiApp.ViewModel
{
    public partial class LoginPageViewModel : MainViewModel
    {
        
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _UserPassword;

        [RelayCommand] // to jest  [ICommand] ale w aktualizacaji CommunityToolkit.Mvvm.Input (8.0.0-preview4 release notes.) zmienili nazwe
        public async void LoginCommand()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Wypełnij nazwę użytkownika", "OK");
            }
            else if (string.IsNullOrEmpty(UserPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Wypełnij hasło", "OK");
            }
        }
        
    }
}
