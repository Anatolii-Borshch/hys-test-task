namespace ScheduleMeetingSystem.Application.Dtos
{
    public class MeetingDto
    {
        public string Title { get; set; } = string.Empty;
        public List<long> ParticipantIds { get; set; } = new List<long>();
        public int DurationMinutes { get; set; }
        public DateTime EarliestStart { get; set; }
        public DateTime LatestEnd { get; set; }
    }
}