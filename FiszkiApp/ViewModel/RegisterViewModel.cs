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

                if (!string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.RepeatPassword) && user.Password != User.RepeatPassword)
                {
                    ErrorMessages += ("\nHasła nie są takie same");
                }
            }
            else
            {
                ErrorMessages = string.Empty;
            }


        }




        [RelayCommand]
        public async void Avatar()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Select an image"
                });

                if (result != null)
                {
                    // Use a `using` statement to ensure the stream is disposed of properly
                    using (var stream = await result.OpenReadAsync())
                    {
                        // Create the ImageSource from the stream and set it to the property
                        User.UploadedImage = ImageSource.FromStream(() => new MemoryStream(ReadFully(stream)));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while uploading the image.", "OK");
            }
        }

        private byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
