﻿using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using Application = Microsoft.Maui.Controls.Application;
using CommunityToolkit.Maui.Converters;
using FiszkiApp.EntityClasses;
using FiszkiApp.dbConnetcion;
using System.IO;
using System.Collections.ObjectModel;
using static FiszkiApp.EntityClasses.AesManaged;

namespace FiszkiApp.ViewModel
{
    public partial class RegisterViewModel : MainViewModel
    {
        public IAsyncRelayCommand LoadCountriesCommand { get; }
        private byte[] imageData;
        private EntityClasses.User user;

        public RegisterViewModel()
        {
            user = new EntityClasses.User();
            LoadCountriesCommand = new AsyncRelayCommand(LoadCountry);
            LoadCountriesCommand.Execute(null);
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

        [ObservableProperty]
        private ObservableCollection<string> countryPicker;

        [ObservableProperty]
        private ImageSource uploadedImage;

        [ObservableProperty]
        private byte[] imageAsByte;

        private async Task LoadCountry()
        {
            var countriesDic = new dbConnetcion.SQLQueries.CountriesDic();
            var countriesWithFlags = await countriesDic.GetCountriesWithFlagsAsync();
            CountryPicker = new ObservableCollection<string>(countriesWithFlags.Select(c => c.Country));
        }

        [RelayCommand]
        public async Task Register()
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
            }else
            {
                ErrorMessages = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(ErrorMessages))
            {
                EncryptionResult encryptionResult = AesManaged.Encryption(User.Password);
                User.EncryptedPassword = encryptionResult.EncryptedData;                
                var creatUser = new EntityClasses.CreatUser();
                string result = await creatUser.UserInsertAsync(User.Name, User.EncryptedPassword, User.FirstName, User.LastName, User.Country, User.Email, User.UploadedImage, User.IsAcceptedPolicy, encryptionResult.IV, encryptionResult.Key);
                if (result == "Rejstracja zakończyła się sukcesem")
                {
                    await Application.Current.MainPage.DisplayAlert("Sukcess", result, "OK");
                    User.Name = null;
                    User.Password = null;
                    User.RepeatPassword = null;
                    User.FirstName = null;
                    User.LastName = null;
                    User.Country = null;
                    User.UploadedImage = null;
                    User.IsAcceptedPolicy = false;
                    User.Email = null;
                    user = new EntityClasses.User();
                }
                else
                {
                    ErrorMessages += (result);                    
                } 
            }
        }

        [RelayCommand]
        public async Task Avatar()
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
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);

                            // Convert the memory stream to a byte array and assign it to the user's UploadedImage
                            user.UploadedImage = memoryStream.ToArray();
                            ImageAsByte = user.UploadedImage;
                        }
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
