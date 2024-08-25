using FiszkiApp.Services;
using FiszkiApp.ViewModel;

namespace FiszkiApp.View;

public partial class ProfilePage : ContentPage
{
    private readonly AuthService _authService;
    public ProfilePage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
        var ProfileViewModel = new ProfileViewModel(_authService);
        BindingContext = ProfileViewModel;
    }
}