using ScheduleMeetingSystem.Application.Dtos;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Contracts.Services
{
    public interface IScheduleMeetingService
    {
        public Task CreateUser(string username);
        public Task<MeetingDto> BookUsersForAMeeting(long[] participantIds, int duration, DateTime earliestStart, DateTime latestEnd);
        public Task<IEnumerable<MeetingDto>> GetUserMeetings(long userId);
    }
}