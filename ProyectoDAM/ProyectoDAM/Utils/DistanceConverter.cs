using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProyectoDAM.Utils
{
    class DistanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int val = (int)(double)value;
            return ((int)val < 1000) ? "Distancia: " + val + " metros." : "Distancia: " + ((double)value / 1000).ToString("F2") + " kilómetros.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
