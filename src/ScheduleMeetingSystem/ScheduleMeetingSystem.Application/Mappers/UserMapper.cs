using ScheduleMeetingSystem.Application.Dtos;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Mappers
{
    public class UserMapper
    {
        public static UserDto MapUserToUserDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
            };
        }
    }
}