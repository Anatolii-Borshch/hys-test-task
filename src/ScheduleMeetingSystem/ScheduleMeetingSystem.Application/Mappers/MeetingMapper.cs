using ScheduleMeetingSystem.Application.Dtos;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Mappers
{
    public static class MeetingMapper
    {
        public static MeetingDto MeetingToMeetingDto(this Meeting meeting)
        {
            return new MeetingDto()
            {
                Title = meeting.Title,
                DurationMinutes = (meeting.EndTime.Second - meeting.StartTime.Second) / 60,
                LatestEnd = meeting.EndTime,
                EarliestStart= meeting.StartTime,
                ParticipantIds = meeting.Users.Select(u => u.Id).ToList(),
            };
        }
    }
}