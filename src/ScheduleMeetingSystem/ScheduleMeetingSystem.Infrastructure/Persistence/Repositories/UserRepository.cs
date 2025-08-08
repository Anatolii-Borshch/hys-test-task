using ScheduleMeetingSystem.Application.Contracts.Repositories;
using ScheduleMeetingSystem.Core.Models;
using ScheduleMeetingSystem.Infrastructure.DbContext;

namespace ScheduleMeetingSystem.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User> ,IUserRepository
    {
        public UserRepository(ScheduleMeetingSystemDbContext context) : base(context)
        {
        }
    }
}