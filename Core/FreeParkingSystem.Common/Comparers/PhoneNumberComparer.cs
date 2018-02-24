using System.Collections.Generic;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Helpers
{
    public class PhoneNumberComparer : IEqualityComparer<PhoneNumber>
    {
        public bool Equals(PhoneNumber x, PhoneNumber y)
        {
            if (x is null)
            {
                return y is null;
            }

            if (y is null) return false;

            return string.Equals(x.Number, y.Number);
        }

        public int GetHashCode(PhoneNumber obj)
        {
            return (obj?.Number != null ? obj.Number.GetHashCode() : 0);
        }

    }
}