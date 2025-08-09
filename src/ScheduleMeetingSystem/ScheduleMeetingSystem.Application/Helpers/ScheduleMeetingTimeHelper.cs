using System.Collections;
using ScheduleMeetingSystem.Core.Models;

namespace ScheduleMeetingSystem.Application.Helpers
{
    public static class ScheduleMeetingTimeHelper
    {
        public static Meeting? FindEarliestMeeting(IEnumerable<Meeting> meetings, int durationMinutes, DateTime earliestStart, DateTime latestEnd)
        {
            var sortedMeetings = meetings.Where(x => x.StartTime >= earliestStart && x.EndTime <= latestEnd)
                .Where(x =>
                {
                    var duration = x.EndTime - x.StartTime;
                    return duration.TotalMinutes >= durationMinutes;
                })
                .OrderBy(x => x.StartTime);
            
            return sortedMeetings.FirstOrDefault();
        }
        
        public static TimeSlot? FindEarliestFreeSlot(IEnumerable<Meeting> meetings, int durationMinutes, DateTime earliestStart, DateTime latestEnd)
        {
            DateTime current = earliestStart.ToUniversalTime();
            
            var busyIntervals = meetings
                .Select(m => (Start: m.StartTime, End: m.EndTime))
                .OrderBy(i => i.Start)
                .ToList();

            foreach (var interval in busyIntervals)
            {
                if (interval.Start > current)
                {
                    if ((interval.Start - current).TotalMinutes >= durationMinutes)
                    {
                        return new TimeSlot(current, current.AddMinutes(durationMinutes));
                    }
                }

                if (interval.End > current)
                {
                    current = interval.End;
                }
            }

            if ((latestEnd - current).TotalMinutes >= durationMinutes)
            {
                return new TimeSlot(current, current.AddMinutes(durationMinutes));
            }

            return null;
        }
    }
}