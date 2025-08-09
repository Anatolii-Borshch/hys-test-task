using ScheduleMeetingSystem.Application.Contracts.Repositories;
using ScheduleMeetingSystem.Application.Contracts.Services;
using ScheduleMeetingSystem.Application.Contracts.Validators;
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
        private readonly IMeetingValidator _validator;

        public ScheduleMeetingService(
            IUserRepository userRepository,
            IMeetingRepository meetingRepository,
            IMeetingValidator validator)
        {
            _userRepository = userRepository;
            _meetingRepository = meetingRepository;
            _validator = validator;
        }
        
        public async Task CreateUser(string username)
        {
            await _validator.ValidateNewUserAsync(username);
            await _userRepository.AddAsync(new User { Name = username });
        }

        public async Task<MeetingDto> BookUsersForAMeeting(long[] participantIds, int duration, DateTime earliestStart, DateTime latestEnd)
        {
            await _validator.ValidateBookingRequestAsync(participantIds, duration, earliestStart, latestEnd);

            var meetings = await _meetingRepository.GetMeetingsWithUsers();
            var validSlot = ScheduleMeetingTimeHelper.FindEarliestMeeting(meetings, duration, earliestStart, latestEnd);

            if (validSlot != null)
            {
                foreach (var id in participantIds)
                {
                    if (validSlot.Users.All(x => x.Id != id))
                    {
                        var user = await _userRepository.GetByIdAsync(id);
                        validSlot.Users.Add(user);
                        return validSlot.MeetingToMeetingDto();
                    }
                }
            }

            var freeTimeSlot = ScheduleMeetingTimeHelper.FindEarliestFreeSlot(meetings, duration, earliestStart, latestEnd);
            if (freeTimeSlot == null)
                throw new ArgumentException("There is no available slot");

            var users = new List<User>();
            foreach (var id in participantIds)
                users.Add(await _userRepository.GetByIdAsync(id));

            var meeting = new Meeting
            {
                StartTime = freeTimeSlot.StartTime,
                EndTime = freeTimeSlot.EndTime,
                Users = users
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