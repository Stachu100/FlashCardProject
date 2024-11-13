using FiszkiApp.ViewModel;

namespace FiszkiApp.View
{
    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class AddFlashcardsPage : ContentPage
    {
        public int CategoryId { get; set; }

        public AddFlashcardsPage()
        {
            InitializeComponent();
        }
    }
}