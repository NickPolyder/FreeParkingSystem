using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Services.Validation;

namespace FreeParkingSystem.Common.Helpers
{
    public class ValidationComponentComparer : IEqualityComparer<IvalidationComponent>
    {
        private TypeEqualityComparer _comparer;

        public ValidationComponentComparer() : this(new TypeEqualityComparer())
        { }

        public ValidationComponentComparer(TypeEqualityComparer comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }
        public bool Equals(IvalidationComponent x, IvalidationComponent y)
        {
            return _comparer.Equals(x, y);
        }

        public int GetHashCode(IvalidationComponent obj)
        {
            return _comparer.GetHashCode(obj);
        }
    }
}