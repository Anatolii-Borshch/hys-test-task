using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Contracts.Repositories
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        public Task<IEnumerable<Meeting>> GetMeetingsWithUsers();
    }
}