using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProyectoDAM.Utils
{
    /// <summary>
    /// Devuelve un color en función del valor entero que reciba
    /// </summary>
    public class ColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((int)value == 0) ? Color.Red : Color.Green;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
