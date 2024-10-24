using FiszkiApp.Services;
using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    public partial class ProfilePage : ContentPage
    {
        private readonly AuthService _authService;
        public ProfilePage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            BindingContext = new ProfileViewModel(_authService);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (BindingContext is ProfileViewModel viewModel)
            {
                await viewModel.OnNavigatedTo(null);
            }
        }
    }
}

