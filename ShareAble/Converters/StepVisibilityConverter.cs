using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareAble.Converters
{
    public class StepVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("Converter1" + value + " " + parameter);
            if (value is int currentStep)
            {
                Debug.WriteLine("inside" + currentStep);
                int targetValue;
                int.TryParse(parameter.ToString(), out targetValue);
                return currentStep == targetValue ? true : false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class StepButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int currentStep && parameter is string buttonType)
            {

                // Adjust the MaxStep value based on your form's maximum step count
                int maxStep = 3;
                return currentStep < maxStep ? true : false;

            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}