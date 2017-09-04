using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class MemberValidationException : Exception
    {
        public IEnumerable<string> MemberNames
        {
            get => Data[nameof(MemberNames)] as IEnumerable<string>;

            private set => Data[nameof(MemberNames)] = value;
        }

        /// <summary>
        /// The type of the object that is Getting validated
        /// </summary>
        public Type ValidationType
        {
            get => Data[nameof(ValidationType)] as Type;
            private set => Data[nameof(ValidationType)] = value;
        }

        /// <summary>
        /// The object that failed the validation.
        /// </summary>
        public object ValidationObject
        {
            get => Data[nameof(ValidationObject)];

            private set => Data[nameof(ValidationObject)] = value;
        }

        public MemberValidationException(object validationObject, string message,
            IEnumerable<string> memberNames = null, Exception innerException = null)
            : base(message, innerException)
        {
            ValidationObject = validationObject;
            ValidationType = validationObject?.GetType();
            MemberNames = memberNames;
        }

        public MemberValidationException()
        {
        }

        public MemberValidationException(string message) : base(message)
        {
        }

        public MemberValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

