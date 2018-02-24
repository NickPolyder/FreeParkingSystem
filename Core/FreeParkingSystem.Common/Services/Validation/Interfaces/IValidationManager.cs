using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace FreeParkingSystem.Common.Services.Validation
{
    public interface IValidationManager : IValidationComponent
    {
        void Add(IValidationComponent composite);

        void AddRange(IEnumerable<IValidationComponent> composites);

        bool Remove(IValidationComponent composite);

        int RemoveRange(IEnumerable<IValidationComponent> composites);
    }
}
