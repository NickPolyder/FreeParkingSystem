using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Services.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ListCountAttribute : ValidationAttribute
    {
        private readonly int _minCount;

        public ListCountAttribute(int minCount)
        {
            _minCount = minCount;
        }

        public override bool IsValid(object value)
        {
            if (value is IEnumerable enumerable)
            {
                var count = 0;
                foreach (var obj in enumerable)
                {
                    count++;

                    if (count >= _minCount)
                        return true;
                }

                return false;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(Res.ValidationMessages.ValidationListCountError, name, _minCount);
            //return base.FormatErrorMessage(name);
        }
    }
}
