namespace ScheduleMeetingSystem.Core.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
    }
}