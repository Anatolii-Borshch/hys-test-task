using System.Collections;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Helpers
{
    public static class ScheduleMeetingTimeHelper
    {
        public static IEnumerable<Meeting> FitByEarliestDateTime(this IEnumerable<Meeting> meetings, TimeZoneInfo timeZone, DateTime now )
        {
            var normalized = meetings
                .Select(x => new Meeting
                {
                    Id = x.Id,
                    StartTime = TimeZoneInfo.ConvertTimeToUtc(x.StartTime, timeZone),
                    EndTime = TimeZoneInfo.ConvertTimeToUtc(x.EndTime, timeZone),
                })
                .OrderBy(x => x.StartTime)
                .ToList();

            var merged = FilteredByTimeGap(normalized).ToList();

            var closestDate = GetClosestDate(merged, now);

            if (closestDate.HasValue)
                return merged.Where(x => x.StartTime >= closestDate.Value);

            return merged;
        }

        private static DateTime? GetClosestDate(IEnumerable<Meeting> meetings, DateTime nowUtc)
        {
            foreach (var meeting in meetings)
            {
                if (nowUtc < meeting.StartTime)
                    return meeting.StartTime;

                if (nowUtc >= meeting.StartTime && nowUtc < meeting.EndTime)
                    return meeting.EndTime;
            }
            return nowUtc;
        }

        private static IEnumerable<Meeting> FilteredByTimeGap(IEnumerable<Meeting> meetings)
        {
            Meeting current = meetings.First();
            foreach (var next in meetings.Skip(1))
            {
                if (next.StartTime <= current.EndTime)
                {
                    if (next.EndTime > current.EndTime)
                    {
                        current.EndTime = next.EndTime;
                    }
                }
                else
                {
                    yield return current;
                    current = next;
                }
            }
            yield return current;
        }
    }
}