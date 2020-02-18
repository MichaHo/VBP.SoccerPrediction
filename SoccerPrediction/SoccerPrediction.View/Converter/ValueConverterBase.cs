using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SoccerPrediction.View
{
    /// <summary>
    /// ein Basiswertkonverter, der die direkte Verwendung von XAML ermöglicht
    /// </ summary>
    /// <typeparam name = "T"> der Typ dieses Wertkonverters </ typeparam>
    public abstract class ValueConverterBase<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private Members
        /// <summary>
        /// eine einzelne statische Instanz dieses Wertkonverters
        /// </ summary>
        private static T _converter = null;
        #endregion

        #region MarkUp Extension Methods
        /// <summary>
        /// liefert eine statische Instanz des Wertkonverters
        /// </ summary>
        /// <param name = "serviceProvider"> der Dienstanbieter </ param>
        /// <returns> </ returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new T());
        }
        #endregion

        #region Value Converter Methods
        /// <summary>
        /// die Methode, um einen Typ in einen anderen zu konvertieren
        /// </ summary>
        /// <param name = "value"> </ param>
        /// <param name = "targetType"> </ param>
        /// <param name = "parameter"> </ param>
        /// <param name = "culture"> </ param>
        /// <returns> </ returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// die Methode, um einen Wert zurück in seinen Quelltyp zu konvertieren
        /// </ summary>
        /// <param name = "value"> </ param>
        /// <param name = "targetType"> </ param>
        /// <param name = "parameter"> </ param>
        /// <param name = "culture"> </ param>
        /// <returns> </ returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion

    }
}
