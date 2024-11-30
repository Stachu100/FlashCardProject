using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    public partial class AddCategoryPage : ContentPage
    {
        public AddCategoryPage()
        {
            InitializeComponent();
            BindingContext = new AddCategoryViewModel();
        }

        private void OnSelectedFrontLanguageChanged(object sender, EventArgs e)
        {
            var viewModel = (AddCategoryViewModel)BindingContext;
            viewModel.OnSelectedFrontLanguageChanged();
        }
    }
}