using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.ApplicationTests.Data
{

    public static class MeetingTimeTestData
    {
        public static IEnumerable<object[]> GetEarliestMeetingTestCases()
        {
            yield return new object[] { "2025-08-08T09:00:00", "2025-08-08T15:00:00", 60, "2025-08-08T10:00:00" };
            yield return new object[] { "2025-08-08T09:00:00", "2025-08-08T15:00:00", 90, null };
            yield return new object[] { "2025-08-08T11:00:00", "2025-08-08T15:00:00", 30, "2025-08-08T12:00:00" };
        }

        public static List<Meeting> GetTestMeetings()
        {
            return new List<Meeting>
            {
                new Meeting()
                {
                    StartTime = DateTime.Parse("2025-08-08T10:00:00"), 
                    EndTime = DateTime.Parse("2025-08-08T11:00:00")
                },
                new Meeting()
                {
                    StartTime = DateTime.Parse("2025-08-08T12:00:00"), 
                    EndTime = DateTime.Parse("2025-08-08T14:00:00")
                },
            };
        }
    }
}