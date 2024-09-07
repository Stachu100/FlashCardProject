using Microsoft.Maui.Controls;
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
    }
}
