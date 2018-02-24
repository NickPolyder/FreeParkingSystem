using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Services.Validation;

namespace FreeParkingSystem.Common.Helpers
{
    public class ValidationComponentEqualityComparer : IEqualityComparer<IValidationComponent>
    {
        private TypeEqualityComparer _comparer;

        public ValidationComponentEqualityComparer() : this(new TypeEqualityComparer())
        { }

        public ValidationComponentEqualityComparer(TypeEqualityComparer comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }
        public bool Equals(IValidationComponent x, IValidationComponent y)
        {
            return _comparer.Equals(x, y);
        }

        public int GetHashCode(IValidationComponent obj)
        {
            return _comparer.GetHashCode(obj);
        }
    }
}