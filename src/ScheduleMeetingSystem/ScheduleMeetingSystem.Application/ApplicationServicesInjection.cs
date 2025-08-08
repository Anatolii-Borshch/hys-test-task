using Microsoft.Extensions.DependencyInjection;
using ScheduleMeetingSystem.Application.Contracts.Services;
using ScheduleMeetingSystem.Application.Implementations.Services;

namespace ScheduleMeetingSystem.Application
{
    public static class ApplicationServicesInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IScheduleMeetingService, ScheduleMeetingService>();
            
            return services;
        }
    }
}