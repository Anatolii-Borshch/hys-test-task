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

        public async Task<MeetingDto> BookUsersForAMeeting(long[] participantIds, int duration, DateTime earliestStart, DateTime latestEnd)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var usersIds = allUsers.Select(x => x.Id).ToHashSet();
            
            bool allExists = participantIds.All(x => usersIds.Contains(x));
            if (!allExists)
                throw new ArgumentException("No participants found");
            
            earliestStart = earliestStart.Kind == DateTimeKind.Utc 
                ? earliestStart 
                : earliestStart.ToUniversalTime();

            latestEnd = latestEnd.Kind == DateTimeKind.Utc 
                ? latestEnd 
                : latestEnd.ToUniversalTime();
            
            if(earliestStart.TimeOfDay < new TimeSpan(9, 0, 0) || latestEnd.TimeOfDay > new TimeSpan(17, 0, 0))
                throw new ArgumentException("Time must be between 9am and 17pm");
            
            var meetings = await _meetingRepository.GetMeetingsWithUsers();
            var validSlot = ScheduleMeetingTimeHelper.FindEarliestMeeting(meetings, duration,earliestStart, latestEnd);

            if (validSlot != null)
            {
                foreach (var id in participantIds)
                {
                    if (validSlot.Users.Any(x => x.Id == id))
                    {
                        continue;
                    }
                    
                    var user = await _userRepository.GetByIdAsync(id);
                    validSlot.Users.Add(user);
                    
                    return validSlot.MeetingToMeetingDto();
                }
            }
            
            var freeTimeSlot = ScheduleMeetingTimeHelper.FindEarliestFreeSlot(meetings, duration, earliestStart, latestEnd);
            if(freeTimeSlot == null)
                throw new ArgumentException("There is no available slot");

            var users = new List<User>();

            foreach (var id in participantIds)
                users.Add(await _userRepository.GetByIdAsync(id)); 

            var meeting = new Meeting()
            {
                StartTime = freeTimeSlot.StartTime,
                EndTime = freeTimeSlot.EndTime,
                Users = users.ToList()
            };
            
            await _meetingRepository.AddAsync(meeting);
            return meeting.MeetingToMeetingDto();
        }

        public async Task<IEnumerable<MeetingDto>> GetUserMeetings(long userId)
        {
            var meetings = await _meetingRepository.GetMeetingsWithUsers();
            
            var usersMeetings = meetings.Where(x => x.Users.Any(y => y.Id == userId));
            
            var mappedMeetings = usersMeetings.Select(x => x.MeetingToMeetingDto());
            return mappedMeetings;
        }
    }
}