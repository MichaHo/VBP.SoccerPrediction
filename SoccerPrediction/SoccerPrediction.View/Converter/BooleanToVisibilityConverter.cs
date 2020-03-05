using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace SoccerPrediction.View
{
    /// <summary>
    /// ein Konverter, der einen Booleschen Wert annimmt und ein <see cref = "Visibility" /> zurückgibt
    /// wird ein Parameter angegeben, wird die <see cref="Visibility"/> umgekehrt
    /// </ summary>
    public class BooleanToVisibilityConverter : ValueConverterBase<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            else
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
