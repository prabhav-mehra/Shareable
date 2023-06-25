using System;
using System.Globalization;

namespace ShareAble.Converters
{
    public class PermissionStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine("Value" + value);
            if (value is PermissionStatus status && status == PermissionStatus.Granted)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

