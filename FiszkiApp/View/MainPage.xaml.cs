using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }
    }

}
