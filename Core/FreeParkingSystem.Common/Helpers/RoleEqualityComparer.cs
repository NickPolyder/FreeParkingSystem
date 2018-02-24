using System;
using System.Collections.Generic;
using System.Text;
using FreeParkingSystem.Common.Models;

namespace FreeParkingSystem.Common.Helpers
{
    public class RoleEqualityComparer : IEqualityComparer<IRole>
    {
        private static RoleEqualityComparer _current;

        public static RoleEqualityComparer Current => (_current ?? (_current = new RoleEqualityComparer()));

        public bool Equals(IRole x, IRole y)
        {
            if (x == null || y == null) return false;
            if (ReferenceEquals(x, y)) return true;

            return x.Id.Equals(y.Id, StringComparison.InvariantCultureIgnoreCase) &&
                   x.Name.Equals(y.Name, StringComparison.InvariantCultureIgnoreCase) &&
                   x.Description.Equals(y.Description, StringComparison.InvariantCultureIgnoreCase) &&
                   x.AccessLevel.Equals(y.AccessLevel);
        }

        public int GetHashCode(IRole obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                var hashCode = (obj.Id != null ? obj.Id.GetHashCode() : 0);
                hashCode *= 397 ^ (obj.Name != null ? obj.Name.GetHashCode() : 0);
                hashCode *= 397 ^ (obj.Description != null ? obj.Description.GetHashCode() : 0);
                hashCode *= 397 ^ (obj.AccessLevel.GetHashCode());
                hashCode *= 397 ^ (obj.IsDeleted.GetHashCode());
                hashCode *= 397 ^ (obj.CreatedAt.GetHashCode());
                hashCode *= 397 ^ (obj.UpdatedAt.GetHashCode());

                return hashCode;
            }
        }
    }
}
