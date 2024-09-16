using FiszkiApp.View;

namespace FiszkiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(FlashCardList), typeof(FlashCardList));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddFlashcardsPage), typeof(AddFlashcardsPage));

            BindingContext = this;

        }

        public Command LogoutCommand => new Command(async () => await LogoutAsync());

        private async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}
