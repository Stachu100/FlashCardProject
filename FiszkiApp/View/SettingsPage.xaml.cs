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
    }
}