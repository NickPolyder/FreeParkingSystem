using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Models.Interfaces;

namespace FreeParkingSystem.Common.Models
{
    public class DaySchedule : IDaySchedule
    {
        public bool Is24HourOpen { get; private set; }

        public List<(TimeSpan Start, TimeSpan End)> OpenHoursList { get; private set; }

        public DayOfWeek Day { get; private set; }
        public DaySchedule()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="openHours">if null or its count == 0 it will be 24hour open</param>
        public DaySchedule(DayOfWeek day, List<(TimeSpan Start, TimeSpan End)> openHours = null)
        {
            Day = day;

            Is24HourOpen = openHours == null || openHours.Count == 0;
            if (!Is24HourOpen)
            {
                OpenHoursList = new List<(TimeSpan Start, TimeSpan End)>();
                openHours?.ForEach(times => AddOpenHours(times.Start, times.End));
            }
        }

        ///<inheritdoc />
        public bool IsOpenNow()
        {
            var now = DateTimeOffset.Now;

            if (now.DayOfWeek != Day) return false;

            if (Is24HourOpen || (OpenHoursList == null || OpenHoursList.Count == 0)) return true;

            var totalMilliseconds = now.TimeOfDay.TotalMilliseconds;
            return OpenHoursList.Any(time => time.Start.TotalMilliseconds >= totalMilliseconds &&
                                             totalMilliseconds < time.End.TotalMilliseconds);
        }

        public void AddOpenHours(TimeSpan start, TimeSpan end)
        {
            if (start == null) throw new ArgumentNullException(nameof(start));
            if (end == null) throw new ArgumentNullException(nameof(end));
            if (end.TotalMilliseconds < start.TotalMilliseconds) throw new ArgumentException($"{nameof(start)} must be before {nameof(end)}");

            //TODO: We should check if the open hours collapse on each other
            OpenHoursList.Add((start, end));
        }
    }
}
