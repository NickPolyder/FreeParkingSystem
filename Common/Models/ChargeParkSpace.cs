using System;

namespace FreeParkingSystem.Common.Models
{
    public struct ChargeParkSpace
    {
        public decimal Cost { get; }

        public int Hour { get; }

        public ChargeParkSpace(decimal cost, int hour)
        {
            if (cost < 0) throw new ArgumentOutOfRangeException(nameof(cost), cost, "Must be above zero");
            if (hour < 0) throw new ArgumentOutOfRangeException(nameof(hour), hour, "Must be above zero");
            Cost = cost;
            Hour = hour;
        }

    }
}
