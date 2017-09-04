using System;
using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Models
{
    public struct PhoneNumber : IEquatable<PhoneNumber>
    {
        [Phone]
        public string Number { get; }

        public PhoneNumberType PhoneType { get; }

        public PhoneNumber(string phoneNumber, PhoneNumberType phoneType)
        {
            Number = phoneNumber;
            PhoneType = phoneType;
        }

        public bool Equals(PhoneNumber other)
        {
            return string.Equals(Number, other.Number);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PhoneNumber && Equals((PhoneNumber)obj);
        }

        public override int GetHashCode()
        {
            return (Number != null ? Number.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"PhoneType: {PhoneType}\nNumber: {Number}";
        }

        public static bool operator ==(PhoneNumber left, PhoneNumber right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PhoneNumber left, PhoneNumber right)
        {
            return !(left == right);
        }
    }
}
