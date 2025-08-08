using ScheduleMeetingSystem.Application.Dtos;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Contracts.Services
{
    public interface IScheduleMeetingService
    {
        public Task CreateUser(string username);
        public Task<IEnumerable<MeetingDto>> GetEarliestMeetings();
        public Task<IEnumerable<MeetingDto>> GetUserMeetings(long userId);
    }
}