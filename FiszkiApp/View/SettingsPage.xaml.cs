using FiszkiApp.Services;
using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsPageViewModel();
        }

        private async void OnPrivacyPolicyTapped(object sender, EventArgs e)
        {
            string policyContent = PrivacyPolicyService.GetPrivacyPolicy();
            policyContent = policyContent.Replace("\n", "\n\n");
            await DisplayAlert("Regulamin U¿ytkowania", policyContent, "OK");
        }
    }
}