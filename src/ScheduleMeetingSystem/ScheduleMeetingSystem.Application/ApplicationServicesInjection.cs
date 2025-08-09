using Microsoft.Extensions.DependencyInjection;
using ScheduleMeetingSystem.Application.Contracts.Services;
using ScheduleMeetingSystem.Application.Contracts.Validators;
using ScheduleMeetingSystem.Application.Implementations.Services;
using ScheduleMeetingSystem.Application.Validators;

namespace ScheduleMeetingSystem.Application
{
    public static class ApplicationServicesInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMeetingValidator, MeetingValidator>();
            services.AddScoped<IScheduleMeetingService, ScheduleMeetingService>();
            
            return services;
        }
    }
}