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

        public override bool Equals(object obj)
        {
            if (!(obj is ChargeParkSpace castedObj)) return false;
            return Cost.Equals(castedObj.Cost) && Hour.Equals(castedObj.Hour);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Cost.GetHashCode() * 397) ^ Hour.GetHashCode();
            }
        }

        public static bool operator ==(ChargeParkSpace left, ChargeParkSpace right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ChargeParkSpace left, ChargeParkSpace right)
        {
            return !(left == right);
        }
    }
}
