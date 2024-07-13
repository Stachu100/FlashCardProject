using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiszkiApp;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FiszkiApp.ViewModel
{
    public partial class RegisterViewModel : MainViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _userPassword;

        [ObservableProperty]
        private string _userRepeatPassword;

        [ObservableProperty]
        private string _userEmail;

        [RelayCommand]
        public async void Register()
        {

        }
    }
}
