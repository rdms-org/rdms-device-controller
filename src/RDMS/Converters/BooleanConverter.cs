using System;
using System.Globalization;
using System.Windows.Data;

namespace RDMS.Converters
{
    /// <summary>
    /// Provides a converter that reverses the boolean value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool?))]
    public class BooleanConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool?)value == null)
            {
                return null;
            }
            else
            {
                return !(bool?)value;
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool?)value == null)
            {
                return null;
            }
            else
            {
                return !(bool)value;
            }
        }
    }
}
