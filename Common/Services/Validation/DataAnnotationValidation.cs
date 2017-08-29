using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class DataAnnotationValidation : IValidationComponent
    {
        public bool CanValidate(object obj)
        {
            return true;
        }

        public IValidationResult Validate(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new Collection<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (Validator.TryValidateObject(obj, validationContext, validationResults, true))
            {
                return ValidationResult.CreateSuccessResult();
            }
            else
            {
                return new ValidationResult(validationResults
                    .Select(tt => new MemberValidationException(obj, tt.ErrorMessage, tt.MemberNames)));
            }
        }
    }
}