using System.Collections.Generic;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Helpers
{
    public class PositionComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position x, Position y)
        {
            if (x is null)
            {
                return y is null;
            }

            if (y is null) return false;

            return x.Latitude.Equals(y.Latitude) && x.Longitude.Equals(y.Longitude);
        }

        public int GetHashCode(Position obj)
        {
            if (obj == null) return 0;
            unchecked
            {
                return (obj.Latitude.GetHashCode() * 397) ^ obj.Longitude.GetHashCode();
            }
        }
    }
}