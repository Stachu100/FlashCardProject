using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FiszkiApp.EntityClasses;

namespace FiszkiApp.ViewModel
{
    public partial class AddFlashcardsPageViewModel : MainViewModel
    {
        private readonly int _categoryId;

        public AddFlashcardsPageViewModel(int categoryId)
        {
            _categoryId = categoryId;
            Flashcards = new ObservableCollection<FlashcardItem>();
        }

        [ObservableProperty]
        private string frontText;

        [ObservableProperty]
        private string backText;

        [ObservableProperty]
        private ObservableCollection<FlashcardItem> flashcards;

        [RelayCommand]
        public void AddNextFlashcard()
        {
            Flashcards.Add(new FlashcardItem
            {
                FrontText = FrontText,
                BackText = BackText
            });

            FrontText = string.Empty;
            BackText = string.Empty;
        }

        [RelayCommand]
        public async Task SubmitFlashcards()
        {
            await SaveFlashcardsToDatabase();
        }

        private Task SaveFlashcardsToDatabase()
        {
            // CHWILOWO PUSTO
            return Task.CompletedTask;
        }
    }
}
