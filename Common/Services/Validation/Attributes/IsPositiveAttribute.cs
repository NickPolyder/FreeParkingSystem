using System;
using System.ComponentModel.DataAnnotations;
using ValResult = System.ComponentModel.DataAnnotations.ValidationResult;
namespace FreeParkingSystem.Common.Services.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
    public sealed class IsPositiveAttribute : ValidationAttribute
    {
        public IsPositiveAttribute() : base(() => Res.ValidationMessages.IsPositive)
        {

        }

        public override bool RequiresValidationContext => true;

        protected override ValResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var result = _canHandle(value, validationContext.ObjectType, validationContext.MemberName);

                return result ?
                    ValResult.Success :
                    new ValResult(FormatErrorMessage(validationContext.MemberName), new[] { validationContext.MemberName });
            }
            catch (Exception ex)
            {
                return new ValResult(ex.Message, new[] { validationContext.MemberName });
            }
        }

        private bool _canHandle(object value, Type type, string memberName)
        {
            var nullable = Nullable.GetUnderlyingType(type);
            if (nullable != null)
            {
                type = nullable;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                    return ((decimal)value) >= 0;
                case TypeCode.Double:
                    return (double)value >= 0;
                case TypeCode.Int16:
                    return (short)value >= 0;
                case TypeCode.Int32:
                    return (int)value >= 0;
                case TypeCode.Int64:
                    return (long)value >= 0;
                case TypeCode.Single:
                    return (float)value >= 0;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    throw new ArgumentException($"The {memberName} must be a numeric value.");
            }
        }
    }
}
