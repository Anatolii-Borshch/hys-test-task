using Microsoft.Extensions.DependencyInjection;
using ScheduleMeetingSystem.Application.Contracts.Repositories;
using ScheduleMeetingSystem.Infrastructure.Persistence.Repositories;

namespace ScheduleMeetingSystem.Infrastructure
{
    public static class InfrastructureServicesInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            
            return services;
        }
    }
}