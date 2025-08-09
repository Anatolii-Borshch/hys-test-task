using System.ComponentModel.DataAnnotations;

namespace ScheduleMeetingSystem.Api.Requests
{
    public class BookMeetingRequest
    {
        [Required(ErrorMessage = "At least one participant is required")]
        [MinLength(1, ErrorMessage = "At least one participant is required")]
        public required long[] ParticipantIds { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than zero")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "EarliestStart is required")]
        public DateTime EarliestStart { get; set; }

        [Required(ErrorMessage = "LatestEnd is required")]
        public DateTime LatestEnd { get; set; }
    }
}