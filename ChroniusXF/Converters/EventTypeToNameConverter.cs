using System;
using System.Globalization;
using ChroniusXF.DataModels;
using Xamarin.Forms;

namespace ChroniusXF.Converters
{
    public class EventTypeToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is EventType eventType)
            {
                return eventType.ToName();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
