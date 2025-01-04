using FiszkiApp.ViewModel;
using FiszkiApp.Services;

namespace FiszkiApp.View
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }

        private async void OnPrivacyPolicyTapped(object sender, EventArgs e)
        {
            string policyContent = PrivacyPolicyService.GetPrivacyPolicy();
            policyContent = policyContent.Replace("\n", "\n\n");
            await DisplayAlert("Regulamin U¿ytkowania", policyContent, "OK");
        }
    }
}