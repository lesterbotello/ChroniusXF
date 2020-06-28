using System;
using System.Windows.Input;
using Prism.Mvvm;
using SQLite;

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
    }
}
