using System;
using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Services.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
    public class ValidateEmailAttribute : ValidationAttribute
    {
        public ValidateEmailAttribute() : base(() => Res.ValidationMessages.ValidationEmailError)
        {

        }

        public override bool IsValid(object value)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str)) return false;

            var atCharacterSplit = str.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);

            //if the length equals to 1 it means it has nothing after the @ (or that the @ character does not exist)
            // if there are more than one @ then it is not a valid e-mail.
            //if the length of the first element of the array is below 3 it has not enough characters before the @ character
            if (atCharacterSplit.Length <= 1 || atCharacterSplit.Length > 2 || atCharacterSplit[0].Length < 3) return false;

            var dotCharacterSplit = atCharacterSplit[1]
                .Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            //if the length equals to 1 it means that it has nothing after the . (or that the dot character does not exist)
            //if the length of the first element of the array is below 3 it has not enough characters before the . character
            //if the length of the second element of the array is below 3 it has not enough characters after the . character
            if (dotCharacterSplit.Length <= 1 || dotCharacterSplit[0].Length < 3 ||
                dotCharacterSplit[1].Length < 3) return false;

            return true;
        }
    }
}
