using System;
using System.Globalization;
using ChroniusXF.DataModels;
using Xamarin.Forms;

namespace ChroniusXF.Converters
{
    public class EventTypeToLocationAwareConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EventType eventType)) return false;
            
            return eventType == EventType.Meeting || eventType == EventType.Party ||
                   eventType == EventType.Seminar;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}