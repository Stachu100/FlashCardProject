using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class FlipCardPage : ContentPage
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

        public FlipCardPage()
        {
            InitializeComponent();
        }

        private void OnCategoryIdSet()
        {
            BindingContext = new FlipCardPageViewModel(CategoryId);
        }
    }
}
