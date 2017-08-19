using System;
using System.Collections.Generic;
using System.Text;

namespace FreeParkingSystem.Common.Models.Interfaces
{
    public interface IDaySchedule
    {
        bool Is24HourOpen { get; }

        List<(TimeSpan Start, TimeSpan End)> OpenHoursList { get; }

        DayOfWeek Day { get; }

        /// <summary>
        /// Checks if for the current day it meets the OpenHour list
        /// </summary>
        /// <returns></returns>
        bool IsOpenNow();

        /// <summary>
        /// Add new entry on OpenHourList
        /// </summary>
        /// <param name="start"> start must be earlier than <paramref name="end"/>.</param>
        /// <param name="end"></param>
        void AddOpenHours(TimeSpan start, TimeSpan end);
    }
}
