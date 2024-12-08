using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiszkiApp.EntityClasses
{
    public partial class UserRegistration : ObservableValidator //ObservableObject
    {

        [ObservableProperty]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [MaxLength(50, ErrorMessage = "Nazwa jest za długa")]
        private string name;

        [ObservableProperty]
        [Required(ErrorMessage = "Imię jest wymagane")]
        [MaxLength(50, ErrorMessage = "Imię jest za długie")]
        private string firstName;

        [ObservableProperty]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(50, ErrorMessage = "Nazwisko jest za długie")]
        private string lastName;

        [ObservableProperty]
        [Required(ErrorMessage = "Kraj jest wymagany")]
        private string country;

        [ObservableProperty]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć conajmniej 8 znaków")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Hasło musi zawierać dużą literę, liczbę oraz znak specjalny")]
        private string password;

        [ObservableProperty]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane")]
        private string repeatPassword;


        [ObservableProperty]
        [Required(ErrorMessage = "Email jest wymagany")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage ="Niepoprawny Email")]
        private string email;

        [ObservableProperty]
        private byte[] uploadedImage;

        [ObservableProperty]
        [Required(ErrorMessage = "Akceptacja polityki prywatności i regulaminu jest wymagana")]
        [CustomValidation(typeof(UserRegistration), nameof(ValidateTrue))]
        private bool isAcceptedPolicy;

        [ObservableProperty]
        private byte[] encryptedPassword;

        public void Validate()
        {
            ValidateAllProperties();
        }
        public static ValidationResult? ValidateTrue(bool value, ValidationContext context)
        {
            return value
                ? ValidationResult.Success
                : new ValidationResult("Akceptacja polityki prywatności i regulaminu jest wymagana");
        }
    }
}