using System.Reflection;
using ScheduleMeetingSystem.Application.Helpers;
using ScheduleMeetingSystem.ApplicationTests.Data;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.ApplicationTests.Tests;

public class ScheduleMeetingTimeHelperTest
{
    [Theory]
    [MemberData(nameof(MeetingTimeTestData.GetEarliestMeetingTestCases), MemberType = typeof(ScheduleMeetingSystem.ApplicationTests.Data.MeetingTimeTestData))]
    public void FindEarliestMeeting_Test(string earliestStartStr, string latestEndStr, int durationMinutes, string expectedStartStr)
    {
        var meetings = MeetingTimeTestData.GetTestMeetings();

        var earliestStart = DateTime.Parse(earliestStartStr);
        var latestEnd = DateTime.Parse(latestEndStr);

        var result = ScheduleMeetingTimeHelper.FindEarliestMeeting(meetings, durationMinutes, earliestStart, latestEnd);

        if (expectedStartStr == null)
        {
            Assert.Null(result);
        }
        else
        {
            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse(expectedStartStr), result!.StartTime);
        }
    }
}