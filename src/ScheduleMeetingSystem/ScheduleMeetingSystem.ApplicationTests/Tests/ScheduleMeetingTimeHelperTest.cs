using System.Reflection;
using ScheduleMeetingSystem.Application.Helpers;
using ScheduleMeetingSystem.ApplicationTests.Data;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.ApplicationTests.Tests;

public class ScheduleMeetingTimeHelperTest
{
    [Theory]
    [MemberData(nameof(MeetingTimeTestData.GetMeetingTimeTestData), MemberType = typeof(ScheduleMeetingSystem.ApplicationTests.Data.MeetingTimeTestData))]
    public void FitByEarliestDateTimeTest(IEnumerable<Meeting> meetings, DateTime nowUtc, DateTime[] expectedStarts)
    {
        var zone = TimeZoneInfo.Utc;

        var result = meetings.FitByEarliestDateTime(zone, nowUtc)
            .Select(x => x.StartTime)
            .ToArray();

        Assert.Equal(expectedStarts, result);
    }
}