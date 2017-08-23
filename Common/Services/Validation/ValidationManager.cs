using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Helpers;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class ValidationManager : IvalidationComponent
    {
        private List<IvalidationComponent> _validations;

        public ValidationManager() : this(_defaultValidation())
        { }

        public ValidationManager(IEnumerable<IvalidationComponent> validationRules)
        {
            _validations = new List<IvalidationComponent>(validationRules);
            var defaultRules = _defaultValidation();
            _validations = _validations.Union(defaultRules, new ValidationComponentComparer()).ToList();
        }

        ///<inheritdoc />
        public IValidationResult Validate(object obj)
        {
            if (_validations.Count == 0)
                throw new InvalidOperationException($"{nameof(_validations)} has zero elements");

            var validationResults = new List<IValidationResult>();
            _validations.ForEach(composite =>
            {
                validationResults.Add(composite.Validate(obj));
            });

            return new ValidationResult(validationResults.Where(tt => tt.Errors != null).SelectMany(tt => tt.Errors));
        }

        public void Add(IvalidationComponent composite)
        {
            if (_validations.IndexOf(composite) < 0)
            {
                _validations.Add(composite);
            }
        }

        public bool Remove(IvalidationComponent composite)
        {
            return _validations.Remove(composite);
        }

        private static IEnumerable<IvalidationComponent> _defaultValidation()
        {
            return new List<IvalidationComponent>
            {
                new DataAnnotationValidation()
            };
        }
    }
}
