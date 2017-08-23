using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeParkingSystem.Common.Services
{
    public class ValidationManager : IValidatationComponent
    {
        private List<IValidatationComponent> _validations;

        public ValidationManager() : this(_defaultValidation())
        { }

        public ValidationManager(IEnumerable<IValidatationComponent> validationRules)
        {
            _validations = new List<IValidatationComponent>(validationRules);
            _validations = _validations.Union(_defaultValidation()).ToList();
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

            return new ValidationResult(validationResults.SelectMany(tt => tt.Errors));
        }

        public void Add(IValidatationComponent composite)
        {
            if (_validations.IndexOf(composite) < 0)
            {
                _validations.Add(composite);
            }
        }

        public bool Remove(IValidatationComponent composite)
        {
            return _validations.Remove(composite);
        }

        private static IEnumerable<IValidatationComponent> _defaultValidation()
        {
            return new List<IValidatationComponent>
            {

            };
        }
    }
}
