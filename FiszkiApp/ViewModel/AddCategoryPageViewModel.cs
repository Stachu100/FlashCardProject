using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp.dbConnetcion.SQLQueries;
using FiszkiApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FiszkiApp.ViewModel
{
    public partial class AddCategoryViewModel : MainViewModel
    {
        private readonly CountriesDic _countriesDic;
        private readonly CategoryQuery _categoryQuery;
        private readonly AuthService _authService;

        public AddCategoryViewModel()
        {
            _countriesDic = new CountriesDic();
            _categoryQuery = new CategoryQuery();
            _authService = new AuthService();

            LanguageLevels = new ObservableCollection<string> { "brak", "A1", "A2", "B1", "B2", "C1", "C2" };
            LoadLanguagesCommand = new AsyncRelayCommand(LoadLanguages);
            LoadLanguagesCommand.Execute(null);
        }

        [ObservableProperty]
        private string categoryName;

        [ObservableProperty]
        private string selectedLanguageLevel;

        [ObservableProperty]
        private string selectedFrontLanguage;

        [ObservableProperty]
        private string selectedBackLanguage;

        [ObservableProperty]
        private ObservableCollection<string> languageLevels;

        [ObservableProperty]
        private ObservableCollection<string> frontLanguages;

        [ObservableProperty]
        private ObservableCollection<string> backLanguages;

        public IAsyncRelayCommand LoadLanguagesCommand { get; }

        public IAsyncRelayCommand SubmitCategoryCommand => new AsyncRelayCommand(SubmitCategory);

        public IAsyncRelayCommand CancelCategoryCommand => new AsyncRelayCommand(CancelCategory);

        // Metoda pobierania języków za pomocą CountriesDic
        private async Task LoadLanguages()
        {
            var languages = await _countriesDic.Countries();
            FrontLanguages = new ObservableCollection<string>(languages);
            BackLanguages = new ObservableCollection<string>(languages);
        }

        private async Task SubmitCategory()
        {
            var (isAuthenticated, userIdString) = await _authService.IsAuthenticatedAsync();

            if (isAuthenticated && int.TryParse(userIdString, out int userId) && userId > 0)
            {
                var result = await _categoryQuery.AddCategoryAsync(userId, CategoryName, SelectedFrontLanguage, SelectedBackLanguage, SelectedLanguageLevel);

                if (result == "Kategoria została dodana pomyślnie")
                {
                    await Shell.Current.GoToAsync("//MainPage");
                }
            }
            else
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        private async Task CancelCategory()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
