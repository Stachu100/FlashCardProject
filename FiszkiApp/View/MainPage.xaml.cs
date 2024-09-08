using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }
    }

}
