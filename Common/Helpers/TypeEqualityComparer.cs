using System.Collections.Generic;

namespace FreeParkingSystem.Common.Helpers
{
    public class TypeEqualityComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null) return false;
            if (ReferenceEquals(x, y)) return true;
            var typeX = x.GetType();
            var typeY = y.GetType();
            return typeX.FullName.Equals(typeY.FullName) && typeX.Assembly.FullName.Equals(typeY.Assembly.FullName);
        }

        public int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            var typeX = obj.GetType();
            unchecked
            {
                return (typeX.FullName.GetHashCode() * 397) ^ typeX.Assembly.FullName.GetHashCode();
            }
        }
    }
}
