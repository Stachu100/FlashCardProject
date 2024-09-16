using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp.dbConnetcion.SQLQueries;
using FiszkiApp.ViewModel;
using FiszkiApp.View;
using FiszkiApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FiszkiApp.Services;
using FiszkiApp.EntityClasses;
using System.Linq;
using System.Collections.Generic;

namespace FiszkiApp.ViewModel
{
    public partial class MainPageViewModel : MainViewModel
    {
        private readonly DatabaseService _databaseService;
        private readonly CountriesUrl _countriesUrl;
        private readonly CategoryQuery _categoryQuery;
        private readonly AuthService _authService;

        public MainPageViewModel()
        {
            _databaseService = App.Database;
            _countriesUrl = new CountriesUrl();
            _categoryQuery = new CategoryQuery();
            _authService = new AuthService();
            Categories = new ObservableCollection<LocalCategoryTable>();
            ViewFlashcardsCommand = new AsyncRelayCommand<LocalCategoryTable>(ViewFlashcardsAsync);
            SendCategoryCommand = new AsyncRelayCommand<LocalCategoryTable>(SendCategoryAsync);
            DeleteCategoryCommand = new AsyncRelayCommand<LocalCategoryTable>(DeleteCategoryAsync);
            LoadCategoriesCommand = new AsyncRelayCommand(LoadCategoriesAsync);
            LoadCategoriesCommand.Execute(null);
        }

        

        [ObservableProperty]
        private ObservableCollection<LocalCategoryTable> categories;

        [ObservableProperty]
        private LocalCategoryTable selectedCategory;

        public IAsyncRelayCommand LoadCategoriesCommand { get; }
        public IAsyncRelayCommand<LocalCategoryTable> SendCategoryCommand { get; }
        public IAsyncRelayCommand<LocalCategoryTable> DeleteCategoryCommand { get; }
        public IAsyncRelayCommand<LocalCategoryTable> ViewFlashcardsCommand { get; }

        [RelayCommand]
        public async Task AddCategory()
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCategoryPage)}");
        }

        private async Task LoadCategoriesAsync()
        {
            var categoriesFromDb = await _databaseService.GetCategoriesAsync();

            var countryUrls = await _countriesUrl.CountriesU();

            Categories.Clear();
            foreach (var category in categoriesFromDb)
            {
                var frontFlag = countryUrls.FirstOrDefault(c => c.Country == category.FrontLanguage).Url;
                var backFlag = countryUrls.FirstOrDefault(c => c.Country == category.BackLanguage).Url;

                category.FrontFlagUrl = frontFlag;
                category.BackFlagUrl = backFlag;

                Categories.Add(category);
            }
        }

        private async Task SendCategoryAsync(LocalCategoryTable category)
        {
            if (category != null)
            {
                var (isAuthenticated, userIdString) = await _authService.IsAuthenticatedAsync();

                if (isAuthenticated && int.TryParse(userIdString, out int userId) && userId > 0)
                {
                    var successMessage = await _categoryQuery.AddCategoryAsync(
                        userId,
                        category.CategoryName,
                        category.FrontLanguage,
                        category.BackLanguage,
                        category.LanguageLevel);

                    if (successMessage == "Kategoria została dodana pomyślnie")
                    {
                        category.IsSent = 1;
                        await _databaseService.UpdateCategoryAsync(category);
                        await LoadCategoriesAsync();
                    }
                }
            }
        }

        private async Task DeleteCategoryAsync(LocalCategoryTable category)
        {
            if (category != null)
            {
                await _databaseService.DeleteCategoryAsync(category);
                Categories.Remove(category);
            }
        }

        private async Task ViewFlashcardsAsync(LocalCategoryTable category)
        {
            if (category != null)
            {
                await Shell.Current.GoToAsync($"{nameof(AddFlashcardsPage)}?CategoryId={category.IdCategory}");
            }
        }
    }
}



