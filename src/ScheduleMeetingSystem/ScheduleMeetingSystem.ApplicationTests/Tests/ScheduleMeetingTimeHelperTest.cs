using System.Reflection;
using ScheduleMeetingSystem.Application.Helpers;
using ScheduleMeetingSystem.ApplicationTests.Data;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.ApplicationTests.Tests;

public class ScheduleMeetingTimeHelperTest
{
    [Theory]
    [MemberData(nameof(MeetingTimeTestData.FindEarliestMeetingCases), MemberType = typeof(MeetingTimeTestData))]
    public void FindEarliestMeeting_ReturnsExpectedTest(
        IEnumerable<Meeting> meetings,
        int durationMinutes,
        DateTime earliestStart,
        DateTime latestEnd,
        DateTime? expectedStart)
    {
        var result = ScheduleMeetingTimeHelper.FindEarliestMeeting(meetings, durationMinutes, earliestStart, latestEnd);
        Assert.Equal(expectedStart, result?.StartTime);
    }

    [Theory]
    [MemberData(nameof(MeetingTimeTestData.FindEarliestFreeSlotCases), MemberType = typeof(MeetingTimeTestData))]
    public void FindEarliestFreeSlot_ReturnsExpectedTest(
        IEnumerable<Meeting> meetings,
        int durationMinutes,
        DateTime earliestStart,
        DateTime latestEnd,
        TimeSlot? expected)
    {
        var result = ScheduleMeetingTimeHelper.FindEarliestFreeSlot(meetings, durationMinutes, earliestStart, latestEnd);

        if (expected == null)
            Assert.Null(result);
        else
        {
            Assert.NotNull(result);
            Assert.Equal(expected.StartTime, result!.StartTime);
            Assert.Equal(expected.EndTime, result.EndTime);
        }
    }
}