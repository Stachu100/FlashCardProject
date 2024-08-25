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

		if (await _authService.IsAuthenticatedAsync())
		{
            //User is logged in
            Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
		else
		{
			//User is not logged in
			//redirtect to loginPage
			// the sing // means no backButton
			Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}
	}
}