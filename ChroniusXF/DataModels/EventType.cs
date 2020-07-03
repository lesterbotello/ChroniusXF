using System;

namespace ChroniusXF.DataModels
{
    public enum EventType
    {
        Reminder,
        Meeting,
        Party,
        Seminar,
        OnlineMeeting,
        Birthday,
        Anniversary,
    }

    public static class EventTypeExtensions
    {
        public static string ToName(this EventType eventType)
        {
            if(eventType == EventType.OnlineMeeting)
            {
                return "Online meeting";
            }

            return Enum.GetName(typeof(EventType), eventType);
        }
    }
}
