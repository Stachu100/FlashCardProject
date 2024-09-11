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
        private readonly CountriesUrl _countriesUrl;
        private readonly AuthService _authService;
        private readonly CategoryQuery _categoryQuery;
       
        public MainPageViewModel()
        {
            _countriesUrl = new CountriesUrl();
            _authService = new AuthService();
            _categoryQuery = new CategoryQuery();
            Categories = new ObservableCollection<Category>();
            ViewFlashcardsCommand = new AsyncRelayCommand<Category>(ViewFlashcardsAsync);
            LoadCategoriesCommand = new AsyncRelayCommand(LoadCategoriesAsync);
            LoadCategoriesCommand.Execute(null);
        }

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private Category selectedCategory;

        public IAsyncRelayCommand LoadCategoriesCommand { get; }
        public IAsyncRelayCommand<Category> ViewFlashcardsCommand { get; }

        [RelayCommand]
        public async Task AddCategory()
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCategoryPage)}");
        }

        private async Task LoadCategoriesAsync()
        {
            var (isAuthenticated, userIdString) = await _authService.IsAuthenticatedAsync();

            if (isAuthenticated && int.TryParse(userIdString, out int userId) && userId > 0)
            {
                var categoriesFromDb = await _categoryQuery.GetCategoriesAsync(userId);

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
            else
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }

        private async Task ViewFlashcardsAsync(Category category)
        {
            if (category != null)
            {
                Console.WriteLine($"ViewFlashcardsAsync called with CategoryID: {category.CategoryID}");

                if (category.CategoryID > 0)
                {
                    await Shell.Current.GoToAsync($"{nameof(AddFlashcardsPage)}?CategoryId={category.CategoryID}");
                }
                else
                {
                    // TESTOWANIE CATEGORY ID CHWILOWO
                    Console.WriteLine("Invalid CategoryID");
                }
            }
        }

    }
}
