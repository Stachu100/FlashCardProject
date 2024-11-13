using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp.dbConnetcion.APIQueries;
using FiszkiApp.Services;
using System.Collections.ObjectModel;
using FiszkiApp.EntityClasses.Models;
using System.Threading.Tasks;
using FiszkiApp.View;
using Microsoft.Maui.Controls;

namespace FiszkiApp.ViewModel
{
    public partial class AddCategoryViewModel : MainViewModel
    {
        private readonly CountriesDic _countriesDic;
        private readonly DatabaseService _databaseService;

        public AddCategoryViewModel()
        {
            _countriesDic = new CountriesDic();
            _databaseService = App.Database;

            LanguageLevel = new ObservableCollection<string> { "Brak", "A1", "A2", "B1", "B2", "C1", "C2" };
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
        private ObservableCollection<string> languageLevel;

        [ObservableProperty]
        private ObservableCollection<string> frontLanguages;

        [ObservableProperty]
        private ObservableCollection<string> backLanguages;

        public IAsyncRelayCommand LoadLanguagesCommand { get; }

        public IAsyncRelayCommand SubmitCategoryCommand => new AsyncRelayCommand(SubmitCategory);

        public IAsyncRelayCommand CancelCategoryCommand => new AsyncRelayCommand(CancelCategory);

        private async Task LoadLanguages()
        {
            var countries = await _countriesDic.GetCountriesWithFlagsAsync();
            FrontLanguages = new ObservableCollection<string>(countries.Select(c => c.Country).ToList());
            BackLanguages = new ObservableCollection<string>(countries.Select(c => c.Country).ToList());
        }

        private async Task SubmitCategory()
        {
            if (string.IsNullOrWhiteSpace(CategoryName) ||
                string.IsNullOrWhiteSpace(SelectedFrontLanguage) ||
                string.IsNullOrWhiteSpace(SelectedBackLanguage) ||
                string.IsNullOrWhiteSpace(SelectedLanguageLevel))
            {
                await Shell.Current.DisplayAlert("Błąd", "Wszystkie pola oznaczone gwiazdką (*) są wymagane.", "OK");
                return;
            }

            if (SelectedFrontLanguage == SelectedBackLanguage)
            {
                await Shell.Current.DisplayAlert("Błąd", "Język przodu i tyłu muszą być różne.", "OK");
                return;
            }

            var newCategory = new LocalCategoryTable
            {
                CategoryName = CategoryName,
                FrontLanguage = SelectedFrontLanguage,
                BackLanguage = SelectedBackLanguage,
                LanguageLevel = SelectedLanguageLevel == "Brak" ? null : SelectedLanguageLevel // Zamiana "brak" na null
            };

            await _databaseService.AddCategoryAsync(newCategory);
            ResetForm();

            await Shell.Current.GoToAsync("//MainPage");
        }

        private async Task CancelCategory()
        {
            ResetForm();

            await Shell.Current.GoToAsync("//MainPage");
        }

        private void ResetForm()
        {
            CategoryName = string.Empty;
            SelectedLanguageLevel = null;
            SelectedFrontLanguage = null;
            SelectedBackLanguage = null;
        }
    }
}