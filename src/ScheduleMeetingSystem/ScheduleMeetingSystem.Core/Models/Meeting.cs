namespace ScheduleMeetingSystem.Core.Models
{
    public class Meeting
    {
        public long Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}