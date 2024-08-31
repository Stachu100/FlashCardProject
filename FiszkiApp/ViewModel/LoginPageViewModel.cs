using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel.DataAnnotations;
using FiszkiApp.dbConnetcion.SQLQueries;
using FiszkiApp.Services;

namespace FiszkiApp.ViewModel
{
    public partial class LoginPageViewModel : MainViewModel
    {
        private readonly AuthService _authService;
        public LoginPageViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        [Required(ErrorMessage = "Nazwa wymagane")]
        private string userName;

        [ObservableProperty]
        [Required(ErrorMessage = "Hasło wymagane")]
        public string userPassword;

        [ObservableProperty]
        private string errorMessages;


       

        [RelayCommand] // to jest  [ICommand] ale w aktualizacaji CommunityToolkit.Mvvm.Input (8.0.0-preview4 release notes.) zmienili nazwe
        public async void LoginCommand()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                var errors = new List<string>();
                foreach (var propertyName in new[] { nameof(UserName), nameof(UserPassword)})
                {             
                    foreach (var error in GetErrors(propertyName))
                    {
                        if (error is ValidationResult validationResult)
                        {
                            errors.Add(validationResult.ErrorMessage);
                        }
                    }
                }

                ErrorMessages = string.Join("\n", errors);
            }
            else
            {
                string result = await _authService.Login(UserName, UserPassword);
                //var loginInQuery = new dbConnetcion.SQLQueries.LogInQuery();
                //string result = await loginInQuery.UserLogIn(;
                if (result != "Hasło lub login jest nie poprawne" && result != "Wystąpił błąd podczas logowania")
                {
                    
                    await Shell.Current.GoToAsync($"//{nameof(View.MainPage)}");
                    ErrorMessages = null;
                }
                else
                {
                    ErrorMessages = result;
                }
            }
        }

    }
}
