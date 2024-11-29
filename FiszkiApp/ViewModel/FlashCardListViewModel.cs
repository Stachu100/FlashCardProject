using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp.dbConnetcion.APIQueries;
using FiszkiApp.ViewModel;
using FiszkiApp.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FiszkiApp.Services;
using FiszkiApp.EntityClasses.Models;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace FiszkiApp.ViewModel
{
    public class FlashCardListViewModel : MainViewModel
    {
        private readonly CategorySearchService _categorySearchService;
        private readonly DatabaseService _databaseService;
        private readonly AuthService _authService;

        public ObservableCollection<LocalCategoryTable> SearchResults { get; set; } = new ObservableCollection<LocalCategoryTable>();
        public string CategorySearch { get; set; }
        public string UserSearch { get; set; }
        public string SelectedLanguageLevel { get; set; }
        public string SelectedLanguage { get; set; }
        public ObservableCollection<string> LanguageLevels { get; set; }
        public ObservableCollection<string> UserLanguages { get; set; } = new ObservableCollection<string>();

        public ICommand SearchCommand { get; }
        public ICommand AddToLocalCommand { get; }

        public FlashCardListViewModel()
        {
            _categorySearchService = new CategorySearchService();
            _databaseService = App.Database;
            _authService = new AuthService();

            LanguageLevels = new ObservableCollection<string> { "Brak", "A1", "A2", "B1", "B2", "C1", "C2" };
            UserLanguages = new ObservableCollection<string>();
            SearchCommand = new Command(async () => await SearchCategoriesAsync());
            AddToLocalCommand = new Command<LocalCategoryTable>(async category => await AddToLocalAsync(category));
        }

        private async Task SearchCategoriesAsync()
        {

            if (UserLanguages == null || UserLanguages.Count == 0)
            {
                return;
            }

            var categories = await _categorySearchService.SearchCategoriesAsync(
                CategorySearch,
                UserSearch,
                SelectedLanguageLevel,
                SelectedLanguage);

            SearchResults.Clear();

            foreach (var category in categories)
            {
                if (UserLanguages.Contains(category.FrontLanguage) || UserLanguages.Contains(category.BackLanguage))
                {
                    SearchResults.Add(new LocalCategoryTable
                    {
                        CategoryName = category.CategoryName,
                        FrontLanguage = category.FrontLanguage,
                        BackLanguage = category.BackLanguage,
                        LanguageLevel = category.LanguageLevel,
                        UserID = category.UserID,
                        IsSent = 1
                    });
                }
            }
        }

        public async Task LoadUserLanguagesAsync()
        {
            var (isAuthenticated, userIdString) = await _authService.IsAuthenticatedAsync();

            if (isAuthenticated && int.TryParse(userIdString, out int userId) && userId > 0)
            {
                var userCountriesService = new UserCountriesService();
                var userCountries = await userCountriesService.GetUserCountriesByUserIdAsync(userId);

                if (userCountries != null)
                {
                    var countriesDic = new CountriesDic();
                    var allCountries = await countriesDic.GetCountriesWithFlagsAsync();

                    var filteredLanguages = allCountries
                        .Where(c => userCountries.Any(uc => uc.ID_Country == c.ID_Country))
                        .Select(c => c.Country)
                        .ToList();

                    UserLanguages.Clear();
                    foreach (var language in filteredLanguages)
                    {
                        UserLanguages.Add(language);
                    }
                }
            }
        }

        private async Task AddToLocalAsync(LocalCategoryTable category)
        {
            if (category != null)
            {
                var (isAuthenticated, userIdString) = await _authService.IsAuthenticatedAsync();

                if (isAuthenticated && int.TryParse(userIdString, out int userId) && userId > 0)
                {
                    category.UserID = userId;
                    await _databaseService.AddCategoryAsync(category);
                }
            }
        }
    }
}