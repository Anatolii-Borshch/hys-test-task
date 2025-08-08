using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.ApplicationTests.Data
{
    public static class MeetingTimeTestData
    {
        public static IEnumerable<object[]> GetMeetingTimeTestData()
        {
            var meetings = new List<Meeting>
            {
                new Meeting
                {
                    StartTime = new DateTime(2025, 6, 20, 9, 0, 0, DateTimeKind.Utc),
                    EndTime   = new DateTime(2025, 6, 20, 10, 0, 0, DateTimeKind.Utc)
                },
                new Meeting
                {
                    StartTime = new DateTime(2025, 6, 20, 11, 0, 0, DateTimeKind.Utc),
                    EndTime   = new DateTime(2025, 6, 20, 12, 0, 0, DateTimeKind.Utc)
                }
            };

            yield return
            [
                meetings,
                new DateTime(2025, 6, 20, 8, 0, 0, DateTimeKind.Utc),
                new[]
                {
                    new DateTime(2025, 6, 20, 9, 0, 0, DateTimeKind.Utc),
                    new DateTime(2025, 6, 20, 11, 0, 0, DateTimeKind.Utc)
                }
            ];

            yield return
            [
                meetings,
                new DateTime(2025, 6, 20, 9, 30, 0, DateTimeKind.Utc),
                new[]
                {
                    new DateTime(2025, 6, 20, 11, 0, 0, DateTimeKind.Utc)
                }
            ];

            yield return
            [
                meetings,
                new DateTime(2025, 6, 20, 10, 30, 0, DateTimeKind.Utc),
                new[]
                {
                    new DateTime(2025, 6, 20, 11, 0, 0, DateTimeKind.Utc)
                }
            ];
        }
    }
}