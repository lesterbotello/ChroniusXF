using System;
using System.Globalization;
using ChroniusXF.DataModels;
using Xamarin.Forms;

namespace ChroniusXF.Converters
{
    public class ChroniusTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Chronius chronius)
            {
                if (chronius.Id == 0)
                    return "New Chronius";

                return $"Edit {chronius.Name}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
