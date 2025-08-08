namespace ScheduleMeetingSystem.Core.Models
{
    public class TimeSlot
    {
        public TimeSlot(DateTime start, DateTime end)
        {
            StartTime = start;
            EndTime = end;
        }
        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}