using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Models.Interfaces;

namespace FreeParkingSystem.Common.Models
{
    public class PaidParkingSpace : ParkingSpace, IPaidParkingSpace
    {
        public IUser Owner { get; set; }

        public string Email { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<ChargeParkSpace> ListOfCharges { get; set; }

        public List<IDaySchedule> DaySchedules { get; set; }

        public bool IsOpenNow()
        {
            if (DaySchedules == null || DaySchedules.Count == 0) return false;
            var now = DateTimeOffset.Now;
            var todaysSchedule = DaySchedules.FirstOrDefault(schedule => schedule.Day == now.DayOfWeek);
            return todaysSchedule != null && todaysSchedule.IsOpenNow();
        }
    }
}
