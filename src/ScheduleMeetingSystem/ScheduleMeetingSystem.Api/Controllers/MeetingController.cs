using Microsoft.AspNetCore.Mvc;
using ScheduleMeetingSystem.Api.Requests;
using ScheduleMeetingSystem.Application.Contracts.Services;

namespace ScheduleMeetingSystem.Api.Controllers
{
    [ApiController]
    [Route("meetings")]
    public class MeetingController : ControllerBase
    {
        private readonly IScheduleMeetingService _scheduleMeetingService;

        public MeetingController(IScheduleMeetingService scheduleMeetingService)
        {
            _scheduleMeetingService = scheduleMeetingService;
        }

        [HttpPost]
        public async Task<IActionResult> BookUsersForMeeting([FromBody] BookMeetingRequest request)
        {
            var meeting = await _scheduleMeetingService.BookUsersForAMeeting(request.ParticipantIds, request.Duration, request.EarliestStart, request.LatestEnd);
            
            return Ok(meeting);
        }
    }
}