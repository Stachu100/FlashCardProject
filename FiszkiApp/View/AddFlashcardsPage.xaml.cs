using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class AddFlashcardsPage : ContentPage
    {
        private int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnCategoryIdSet();
            }
        }

        public AddFlashcardsPage()
        {
            InitializeComponent();
        }

        private void OnCategoryIdSet()
        {
            BindingContext = new AddFlashcardsPageViewModel(CategoryId);
        }
    }
}
