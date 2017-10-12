﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class ValidationResult : IValidationResult
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
            if (errors == null) throw new ArgumentNullException(nameof(errors));
            var errs = errors.ToArray();
            IsValid = errs.Length <= 0;
            Errors = errs;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ValidationResult)) return false;
            var errors = Errors.ToArray();
            var castedObj = (ValidationResult)obj;
            var castedErrors = castedObj.Errors.ToArray();
            return IsValid == castedObj.IsValid && errors.Length == castedErrors.Length && errors.Equals(castedErrors);
        }

        public override int GetHashCode()
        {
            var errors = Errors.ToArray();
            return (IsValid.GetHashCode() * 397) ^ (errors.GetHashCode());
        }

        public static bool operator ==(ValidationResult left, ValidationResult right)
        {
            return left?.Equals(right) ?? false;
        }

        public static bool operator !=(ValidationResult left, ValidationResult right)
        {
            return !(left == right);
        }

        public static ValidationResult CreateSuccessResult()
        {
            return new ValidationResult(true);
        }

        public static ValidationResult CreateErrorResult(Exception ex)
        {
            return new ValidationResult(false, new[] { ex });
        }

        public static ValidationResult CreateErrorResult(IEnumerable<Exception> exceptions)
        {
            return new ValidationResult(false, exceptions);
        }
    }
}