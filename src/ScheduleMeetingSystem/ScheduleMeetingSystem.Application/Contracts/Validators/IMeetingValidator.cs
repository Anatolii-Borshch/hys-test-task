namespace ScheduleMeetingSystem.Application.Contracts.Validators
{
    public interface IMeetingValidator
    {
        Task ValidateNewUserAsync(string username);
        Task ValidateBookingRequestAsync(long[] participantIds, int duration, DateTime earliestStart, DateTime latestEnd);
    }
}