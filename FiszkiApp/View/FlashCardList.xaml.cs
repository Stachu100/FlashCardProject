using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    public partial class FlashCardList : ContentPage
    {
        public FlashCardList()
        {
            InitializeComponent();
            BindingContext = new FlashCardListViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as FlashCardListViewModel;
            if (viewModel != null)
            {
                await viewModel.LoadUserLanguagesAsync();
            }
        }
    }
}