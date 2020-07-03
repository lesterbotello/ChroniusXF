using System;
using Prism.Mvvm;
using SQLite;
using Xamarin.Essentials;

namespace ChroniusXF.DataModels
{
    public class Chronius : BindableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime StartingDate { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public int EventTypeId { get; set; }

        [Ignore]
        public bool IsActive
        {
            get
            {
                if (TargetDate == null)
                    return false;

                var distance = TargetDate - DateTime.Now;
                return distance.Seconds > 0;
            }
        }

        string _displayText;

        [Ignore]
        public string DisplayText
        {
            get => _displayText;
            set => SetProperty(ref _displayText, value);
        }

        [Ignore]
        public Location Location
        {
            get
            {
                if(Longitude != null && Latitude != null)
                {
                    return new Location(Latitude.Value, Longitude.Value);
                }

                return null;
            }
        }

        [Ignore]
        public EventType EventType => (EventType)EventTypeId;
    }
}
