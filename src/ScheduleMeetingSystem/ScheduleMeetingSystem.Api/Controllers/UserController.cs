using Microsoft.AspNetCore.Mvc;
using ScheduleMeetingSystem.Application.Contracts.Services;

namespace ScheduleMeetingSystem.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IScheduleMeetingService _scheduleMeetingService;

        public UserController(IScheduleMeetingService scheduleMeetingService)
        {
            _scheduleMeetingService = scheduleMeetingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] string name)
        {
            await _scheduleMeetingService.CreateUser(name);
            return Created();
        }

        [HttpGet("{userId:int}/meetings")]
        public async Task<IActionResult> GetUsersMeetings(int userId)
        {
            var meetings = await _scheduleMeetingService.GetUserMeetings(userId);
            return Ok(meetings);
        }
    }
}