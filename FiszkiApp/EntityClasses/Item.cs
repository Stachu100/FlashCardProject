using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FiszkiApp.EntityClasses
{
    public class Item : ObservableObject
    {
        public int ID_Country { get; set; }
        public string Name { get; set; }
        public ImageSource Image { get; set; }
    }
}