using FiszkiApp.Services;

namespace FiszkiApp.View;

public partial class LoadingPage : ContentPage
{
	private readonly AuthService _authService;
	public LoadingPage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
        var (isAuthenticated, userID) = await _authService.IsAuthenticatedAsync();
        if (isAuthenticated)
		{
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
		else
		{
			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}