using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp;

namespace FiszkiApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public bool _isBusy;

        [ObservableProperty]
        public bool _title;


        [RelayCommand]
        Task NavigateToRegister() => Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
