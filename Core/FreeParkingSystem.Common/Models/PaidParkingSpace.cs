using FreeParkingSystem.Common.Services.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeParkingSystem.Common.Models
{
    public class PaidParkingSpace : ParkingSpace, IPaidParkingSpace
    {
        [Required]
        public IUser Owner { get; set; }

        public string Email { get; set; }

        [ListCount(1)]
        public List<PhoneNumber> PhoneNumbers { get; set; }

        [ListCount(1)]
        public List<ChargeParkSpace> ListOfCharges { get; set; }

        [ListCount(1)]
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
