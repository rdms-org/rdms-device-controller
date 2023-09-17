using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace RDMS.Converters
{
    /// <summary>
    /// Provides a converter that converts <see cref="bool"/> and <see cref="Visibility"/> values to each other.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility)value)
            {
                case Visibility.Visible:
                case Visibility.Collapsed:
                    return true;
                case Visibility.Hidden:
                    return false;
                default:
                    return false;
            }
        }
    }
}
