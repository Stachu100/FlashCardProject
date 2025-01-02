using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace FiszkiApp.ViewModel
{
    public partial class SettingsPageViewModel : MainViewModel
    {
        [ObservableProperty]
        private string customColor;

        public ICommand SetColorCommand { get; }
        public ICommand SetTextColorCommand { get; }
        public ICommand SetCustomColorCommand { get; }
        public IAsyncRelayCommand DeleteDataCommand { get; }

        public SettingsPageViewModel()
        {
            SetColorCommand = new RelayCommand<string>(SetColor);
            SetTextColorCommand = new RelayCommand<string>(SetTextColor);
            SetCustomColorCommand = new RelayCommand(SetCustomColor);
            DeleteDataCommand = new AsyncRelayCommand(DeleteData);
        }

        private void SetColor(string color)
        {
            Preferences.Set("FlashcardBackgroundColor", color);
        }

        private void SetTextColor(string color)
        {
            Preferences.Set("FlashcardTextColor", color);
        }

        private void SetCustomColor()
        {
            if (IsValidColor(CustomColor))
            {
                Preferences.Set("FlashcardBackgroundColor", CustomColor);
            }
            else
            {
                Shell.Current.DisplayAlert("B³¹d", "Podano nieprawid³owy kolor. U¿yj formatu #RRGGBB.", "OK");
            }
        }

        private bool IsValidColor(string color)
        {
            return !string.IsNullOrEmpty(color) && (color.StartsWith("#") && (color.Length == 7 || color.Length == 9));
        }

        private async Task DeleteData()
        {
            await Shell.Current.DisplayAlert("Potwierdzenie", "Dane zosta³y usuniête.", "OK");
        }
    }
}