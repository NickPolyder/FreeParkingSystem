using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Helpers;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class ValidationManager : IValidationManager
    {
        private readonly List<IValidationComponent> _validations;

        private readonly ValidationComponentEqualityComparer _comparer = new ValidationComponentEqualityComparer();

        public ValidationManager() : this(_defaultValidation())
        { }

        public ValidationManager(IEnumerable<IValidationComponent> validationRules)
        {
            _validations = new List<IValidationComponent>(validationRules);
            var defaultRules = _defaultValidation();
            _validations = _validations.Union(defaultRules, _comparer).ToList();
        }

        ///<inheritdoc />
        public IValidationResult Validate(object obj)
        {
            if (_validations.Count == 0)
                throw new InvalidOperationException($"{nameof(_validations)} has zero elements");

            var validationResults = new List<IValidationResult>();
            _validations.Where(tt => tt.CanValidate(obj)).ToList().ForEach(composite =>
              {
                  validationResults.Add(composite.Validate(obj));
              });

            return new ValidationResult(validationResults.Where(tt => tt.Errors != null).SelectMany(tt => tt.Errors));
        }

        public bool CanValidate(object obj)
        {
            return _validations.Any(tt => tt.CanValidate(obj));
        }

        public void Add(IValidationComponent composite)
        {
            if (!_validations.Any(validation => _comparer.Equals(composite, validation)))
            {
                _validations.Add(composite);
            }
        }

        public void AddRange(IEnumerable<IValidationComponent> composites)
        {
            foreach (var composite in composites)
            {
                Add(composite);
            }
        }

        public bool Remove(IValidationComponent composite)
        {
            return _validations.RemoveAll(validation => _comparer.Equals(composite, validation)) > 0;
        }

        public int RemoveRange(IEnumerable<IValidationComponent> composites)
        {
            var counter = 0;
            foreach (var composite in composites)
            {
                if (Remove(composite))
                {
                    counter++;
                }
            }

            return counter;
        }

        private static IEnumerable<IValidationComponent> _defaultValidation()
        {
            return new List<IValidationComponent>
            {
                new DataAnnotationValidation()
            };
        }
    }
}
