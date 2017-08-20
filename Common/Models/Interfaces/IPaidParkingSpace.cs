using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace FreeParkingSystem.Common.Models
{
    public interface IPaidParkingSpace : IParkingSpace
    {
        /// <summary>
        /// The Owner of the Parking Place
        /// </summary>
        IUser Owner { get; set; }

        string Email { get; set; }

        List<PhoneNumber> PhoneNumbers { get; set; }

        List<ChargeParkSpace> ListOfCharges { get; set; }

        List<IDaySchedule> DaySchedules { get; set; }

        /// <summary>
        /// Checks all the Daily Schedules to see if it is open
        /// </summary>
        /// <returns></returns>
        bool IsOpenNow();
    }
}
