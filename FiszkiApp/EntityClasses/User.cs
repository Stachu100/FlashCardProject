using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace FiszkiApp.EntityClasses
{
    public partial class User : ObservableValidator //ObservableObject
    {

        [ObservableProperty]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        private string name;

        [ObservableProperty]
        [Required(ErrorMessage = "Imię jest wymagane")]
        private string firstName;

        [ObservableProperty]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        private string lastName;

        [ObservableProperty]
        [Required(ErrorMessage = "Kraj jest wymagany")]
        private string country;

        [ObservableProperty]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć conajmniej 8 znaków")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_])$", ErrorMessage = "Hasło musi zawierać dużą literę, znak specjalny")]
        private string password;

        [ObservableProperty]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagana")]
        private string repeatPassword;


        [ObservableProperty]
        [Required(ErrorMessage = "Email jest wymagany")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage ="Niepoprawny Email")]
        private string email;

        [ObservableProperty]
        private ImageSource uploadedImage;
        public void Validate()
        {
            ValidateAllProperties();
        }
    }
}

