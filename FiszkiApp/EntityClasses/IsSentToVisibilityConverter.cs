using System;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace FiszkiApp.EntityClasses
{
    public class IsSentToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int isSent)
            {
                return isSent == 0 ? true : false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
