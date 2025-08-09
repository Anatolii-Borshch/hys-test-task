using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.ApplicationTests.Data
{
    public static class MeetingTimeTestData
    {
        public static IEnumerable<object[]> FindEarliestMeetingCases()
        {
            var now = DateTime.UtcNow;

            yield return
            [
                new[]
                {
                    new Meeting { StartTime = now.AddHours(1), EndTime = now.AddHours(2) }
                },
                30, now, now.AddHours(5),
                now.AddHours(1)
            ];

            yield return
            [
                new[]
                {
                    new Meeting { StartTime = now.AddHours(3), EndTime = now.AddHours(4) },
                    new Meeting { StartTime = now.AddHours(1), EndTime = now.AddHours(2) }
                },
                30, now, now.AddHours(5),
                now.AddHours(1)
            ];

            yield return
            [
                new[]
                {
                    new Meeting { StartTime = now.AddHours(1), EndTime = now.AddMinutes(70) }
                },
                120, now, now.AddHours(5),
                (DateTime?)null
            ];
        }
        
        public static IEnumerable<object[]> FindEarliestFreeSlotCases()
        {
            var now = DateTime.UtcNow;

            yield return
            [
                Array.Empty<Meeting>(),
                60, now, now.AddHours(4),
                new TimeSlot(now, now.AddHours(1))
            ];

            yield return new object[]
            {
                new[]
                {
                    new Meeting { StartTime = now.AddHours(2), EndTime = now.AddHours(3) }
                },
                60, now, now.AddHours(4),
                new TimeSlot(now, now.AddHours(1))
            };
            
            yield return
            [
                new[]
                {
                    new Meeting { StartTime = now, EndTime = now.AddHours(2) }
                },
                180, now, now.AddHours(4),
                null
            ];
        }
    }
}