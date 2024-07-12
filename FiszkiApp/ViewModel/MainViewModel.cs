using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp;

namespace FiszkiApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        string login;

        [NotifyPropertyChangedFor(nameof(FullName))]
        [ObservableProperty]
        string password;

        public string FullName => $"{Login} {Password}";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [RelayCommand]
        void Tap()
        {
            IsBusy = true;

            Console.WriteLine(FullName);

            IsBusy = false;
        }

        [RelayCommand]
        Task NavigateToRegister() => Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
