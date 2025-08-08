namespace ScheduleMeetingSystem.Api.Requests
{
    public class BookMeetingRequest
    {
        public required long[] ParticipantIds { get; set; }
        public int Duration { get; set; }
        public DateTime EarliestStart { get; set; }
        public DateTime LatestEnd { get; set; }
    }
}