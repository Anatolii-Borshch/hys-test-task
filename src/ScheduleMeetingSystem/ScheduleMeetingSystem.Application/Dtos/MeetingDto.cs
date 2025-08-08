namespace ScheduleMeetingSystem.Application.Dtos
{
    public class MeetingDto
    {
        public List<long> ParticipantIds { get; set; } = new List<long>();
        public int DurationMinutes { get; set; }
        public DateTime EarliestStart { get; set; }
        public DateTime LatestEnd { get; set; }
    }
}