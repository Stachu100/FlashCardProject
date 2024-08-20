using CommunityToolkit.Maui.Behaviors;
using FiszkiApp.ViewModel;
using Microsoft.Maui.Controls;

namespace FiszkiApp.View
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

            var loginPageViewModel = new LoginPageViewModel();
            BindingContext = loginPageViewModel;

        }

        private async void OnLabelTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }

}