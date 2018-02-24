using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Models
{
    public class PhoneNumber
    {
        [Phone]
        public string Number { get; }

        public PhoneNumberType PhoneType { get; }

        public PhoneNumber(string phoneNumber, PhoneNumberType phoneType)
        {
            Number = phoneNumber;
            PhoneType = phoneType;
        }

        public override string ToString()
        {
            return $"PhoneType: {PhoneType}\nNumber: {Number}";
        }
    }
}
