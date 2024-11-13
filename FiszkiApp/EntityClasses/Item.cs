using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FiszkiApp.EntityClasses
{
    public class Item : ObservableObject
    {
        public string Name { get; set; }
        public ImageSource Image { get; set; }
    }
}