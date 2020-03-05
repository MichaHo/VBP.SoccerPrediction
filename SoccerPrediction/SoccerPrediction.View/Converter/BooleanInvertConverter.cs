using System;
using System.Globalization;

namespace SoccerPrediction.View
{
    /// <summary>
    /// Ein Konverter, der einen Booleschen Wert aufnimmt und ihn invertiert
    /// </ summary>
    public class BooleanInvertConverter : ValueConverterBase<BooleanInvertConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
