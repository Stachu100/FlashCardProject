using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiszkiApp.View;
using System.Threading.Tasks;


namespace FiszkiApp.ViewModel
{
    public partial class MainPageViewModel : MainViewModel
    {
        public MainPageViewModel()
        {
        }
        [RelayCommand]
        public async Task AddCategory()
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCategoryPage)}");
        }
    }
}
