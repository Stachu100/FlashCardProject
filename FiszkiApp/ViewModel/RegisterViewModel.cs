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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiszkiApp.ViewModel
{
    public partial class RegisterViewModel : MainViewModel
    {

        private EntityClasses.User user;


        public RegisterViewModel()
        {
            user = new EntityClasses.User();
        }

        
        public EntityClasses.User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }

        string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        [RelayCommand]
        public async void Register()
        {


            if (!Regex.IsMatch(User.Email, pattern))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Nie poprawny email", "OK");

            }            
        }
        [RelayCommand]
        public async void Avatar()
        {
        }
    }
}
