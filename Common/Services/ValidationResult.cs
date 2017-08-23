using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeParkingSystem.Common.Services
{
    public struct ValidationResult : IValidationResult
    {
        public bool IsValid { get; }

        public IEnumerable<Exception> Errors { get; }

        public ValidationResult(bool isValid, IEnumerable<Exception> errors = null)
        {
            IsValid = isValid;
            Errors = errors;
        }

        public ValidationResult(IEnumerable<Exception> errors)
        {
            IsValid = errors?.Count() > 0;
            Errors = errors;
        }
    }
}