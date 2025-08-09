using ScheduleMeetingSystem.Application.Contracts.Repositories;
using ScheduleMeetingSystem.Application.Contracts.Validators;

namespace ScheduleMeetingSystem.Application.Validators
{
    public class MeetingValidator : IMeetingValidator
    {
        private readonly IUserRepository _userRepository;

        public MeetingValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateNewUserAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            var users = await _userRepository.GetAllAsync();
            if (users.Any(x => x.Name == username))
                throw new ArgumentException("Username is already taken");
        }

        public async Task ValidateBookingRequestAsync(long[] participantIds, int duration, DateTime earliestStart, DateTime latestEnd)
        {
            var allUsers = await _userRepository.GetAllAsync();
            var usersIds = allUsers.Select(x => x.Id).ToHashSet();
            if (!participantIds.All(x => usersIds.Contains(x)))
                throw new ArgumentException("No participants found");

            earliestStart = EnsureUtc(earliestStart);
            latestEnd = EnsureUtc(latestEnd);

            if (earliestStart.TimeOfDay < new TimeSpan(9, 0, 0) || latestEnd.TimeOfDay > new TimeSpan(17, 0, 0))
                throw new ArgumentException("Time must be between 9am and 17pm");
        }

        private DateTime EnsureUtc(DateTime dateTime)
        {
            return dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        }
    }
}