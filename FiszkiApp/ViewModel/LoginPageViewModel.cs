using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiszkiApp.ViewModel
{
    public partial class LoginPageViewModel : MainViewModel
    {
        [ObservableProperty]
        private string _userName;
        
        [ObservableProperty]
        private string _password;

        [RelayCommand] // to jest  [ICommand] ale w aktualizacaji CommunityToolkit.Mvvm.Input (8.0.0-preview4 release notes.) zmienili nazwe
        public async void Login()
        {

        }
    }
}
