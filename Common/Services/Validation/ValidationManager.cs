using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Helpers;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class ValidationManager : IValidationComponent
    {
        private List<IValidationComponent> _validations;

        public ValidationManager() : this(_defaultValidation())
        { }

        public ValidationManager(IEnumerable<IValidationComponent> validationRules)
        {
            _validations = new List<IValidationComponent>(validationRules);
            var defaultRules = _defaultValidation();
            _validations = _validations.Union(defaultRules, new ValidationComponentComparer()).ToList();
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
            if (_validations.IndexOf(composite) < 0)
            {
                _validations.Add(composite);
            }
        }

        public bool Remove(IValidationComponent composite)
        {
            return _validations.Remove(composite);
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
