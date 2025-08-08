using ScheduleMeetingSystem.Application.Contracts.Repositories;
using ScheduleMeetingSystem.Application.Contracts.Services;
using ScheduleMeetingSystem.Application.Dtos;
using ScheduleMeetingSystem.Application.Helpers;
using ScheduleMeetingSystem.Application.Mappers;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Implementations.Services
{
    public class ScheduleMeetingService : IScheduleMeetingService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMeetingRepository _meetingRepository;

        public ScheduleMeetingService(IUserRepository userRepository, IMeetingRepository meetingRepository)
        {
            _userRepository = userRepository;
            _meetingRepository = meetingRepository;
        }
        
        public async Task CreateUser(string username)
        {
            if(string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            
            var users = await _userRepository.GetAllAsync();
            var isUserExist = users.FirstOrDefault(x => x.Name == username);

            if (isUserExist != null)
                throw new ArgumentException("Username is already taken");
            
            await _userRepository.AddAsync(new User() { Name = username });
        }
 
        public async Task<IEnumerable<MeetingDto>> GetEarliestMeetings()
        {
            var meetings = await _meetingRepository.GetAllAsync();
            
            var earliestMeeting = meetings.FitByEarliestDateTime(TimeZoneInfo.Utc, DateTime.UtcNow);
            
            var mappedMeetings = earliestMeeting.Select(x => x.MeetingToMeetingDto());
            return mappedMeetings;
        }

        public async Task<IEnumerable<MeetingDto>> GetUserMeetings(long userId)
        {
            var meetings = await _meetingRepository.GetAllAsync();
            
            var usersMeetings = meetings.Where(x => x.Users.Any(y => y.Id == userId));
            
            var mappedMeetings = usersMeetings.Select(x => x.MeetingToMeetingDto());
            return mappedMeetings;
        }
    }
}