using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using Application = Microsoft.Maui.Controls.Application;

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
            set
            {
                SetProperty(ref user, value);

            }
        }

        [ObservableProperty]
        private string errorMessages;

        [RelayCommand]
        public async void Register()
        {
            User.Validate();
            if (User.HasErrors)
            {
                var errors = new List<string>();
                foreach (var propertyName in new[] { nameof(User.Name), nameof(User.FirstName), nameof(User.LastName), nameof(User.Password), nameof(User.RepeatPassword), nameof(User.Country), nameof(User.Email) })
                {
                    foreach (var error in User.GetErrors(propertyName))
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
                ErrorMessages = string.Empty;
            }


        }




        [RelayCommand]
        public async void Avatar()
        {

        }
    }
}
