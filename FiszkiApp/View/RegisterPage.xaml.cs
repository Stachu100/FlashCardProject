using FiszkiApp.ViewModel;

namespace FiszkiApp;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        //BindingContext = new RegisterViewModel();
        LoadCountry();
    }
    private void LoadCountry()
    {
        // Przyk³adowa lista p³ci
        var genders = new List<string>
            {
                "Polska",
                "England"                
            };

        CountryPicker.ItemsSource = genders;
    }
}
