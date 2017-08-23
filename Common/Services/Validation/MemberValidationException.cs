using System;
using System.Collections.Generic;
using System.Text;

namespace FreeParkingSystem.Common.Services.Validation
{
    public class MemberValidationException : Exception
    {
        public IEnumerable<string> MemberNames
        {
            get { return Data[nameof(MemberNames)] as IEnumerable<string>; }

            private set { Data[nameof(MemberNames)] = value; }
        }

        /// <summary>
        /// The type of the object that is Getting validated
        /// </summary>
        public Type ValidationType
        {
            get { return Data[nameof(ValidationType)] as Type; }
            private set { Data[nameof(ValidationType)] = value; }
        }

        /// <summary>
        /// The object that failed the validation.
        /// </summary>
        public object ValidationObject
        {
            get { return Data[nameof(ValidationObject)]; }

            private set { Data[nameof(ValidationObject)] = value; }
        }

        public MemberValidationException(object validationObject, string message,
            IEnumerable<string> memberNames = null, Exception innerException = null)
            : base(message, innerException)
        {
            ValidationObject = validationObject;
            ValidationType = validationObject?.GetType();
            MemberNames = memberNames;
        }

    }
}
