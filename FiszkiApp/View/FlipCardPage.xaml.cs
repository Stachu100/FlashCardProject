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

        private bool _isAnimating = false;

        private async void OnFlipCardTapped(object sender, EventArgs e)
        {
            if (_isAnimating) return;
            _isAnimating = true;

            var flipView = FlashCardFrame;

            await flipView.RotateYTo(90, 300);

            var viewModel = BindingContext as FlipCardPageViewModel;
            if (viewModel != null)
            {
                viewModel.IsFrontVisible = !viewModel.IsFrontVisible;
                viewModel.IsBackVisible = !viewModel.IsBackVisible;
            }

            await flipView.RotateYTo(180, 300);

            flipView.RotationY = 0;

            _isAnimating = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var savedColor = Preferences.Get("FlashcardBackgroundColor", "#512BD4");
            Console.WriteLine($"Pobrany kolor: {savedColor}");
            FlashCardFrame.BackgroundColor = Color.FromArgb(savedColor);
        }
    }
}