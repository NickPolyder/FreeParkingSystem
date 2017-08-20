using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Models
{
    public class DaySchedule : IDaySchedule
    {
        public bool Is24HourOpen { get; private set; }

        public List<(TimeSpan Start, TimeSpan End)> OpenHoursList { get; private set; }

        public DayOfWeek Day { get; private set; }
        public DaySchedule() : this(DayOfWeek.Sunday)
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
            return OpenHoursList.Any(time => totalMilliseconds >= time.Start.TotalMilliseconds &&
                                             totalMilliseconds < time.End.TotalMilliseconds);
        }

        public void AddOpenHours(TimeSpan start, TimeSpan end)
        {
            if (Is24HourOpen) return;
            if (OpenHoursList == null) throw new ArgumentNullException(nameof(OpenHoursList));
            if (end.TotalMilliseconds <= start.TotalMilliseconds) throw new ArgumentException($"{nameof(start)} must be before {nameof(end)}");

            #region If the start and the end times are inside of a current Open Hour List
            if (OpenHoursList.Any(time => start.TotalMilliseconds > time.Start.TotalMilliseconds &&
                                          end.TotalMilliseconds < time.End.TotalMilliseconds))
            {
                return;
            }
            #endregion

            int index;

            #region If the Start is the same or higher but the end is higher
            index = OpenHoursList.FindIndex(time => start.TotalMilliseconds >= time.Start.TotalMilliseconds &&
                                                    start.TotalMilliseconds <= time.End.TotalMilliseconds &&
                                                    end.TotalMilliseconds > time.End.TotalMilliseconds);
            if (index >= 0)
            {
                OpenHoursList[index] = (OpenHoursList[index].Start, end);
                return;
            }


            #endregion

            #region If The End Is Lower or the same and the Start is Lower
            index = OpenHoursList.FindIndex(time => start.TotalMilliseconds < time.Start.TotalMilliseconds &&
                                                    end.TotalMilliseconds >= time.Start.TotalMilliseconds &&
                                                    end.TotalMilliseconds <= time.End.TotalMilliseconds);
            if (index >= 0)
            {
                OpenHoursList[index] = (start, OpenHoursList[index].End);
                return;
            }
            #endregion

            OpenHoursList.Add((start, end));
        }
    }
}
