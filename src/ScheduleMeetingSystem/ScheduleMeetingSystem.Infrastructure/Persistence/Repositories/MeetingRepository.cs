using ScheduleMeetingSystem.Application.Contracts.Repositories;
using ScheduleMeetingSystem.Core.Models;
using ScheduleMeetingSystem.Infrastructure.DbContext;

namespace ScheduleMeetingSystem.Infrastructure.Persistence.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(ScheduleMeetingSystemDbContext context) : base(context)
        {
        }
    }
}