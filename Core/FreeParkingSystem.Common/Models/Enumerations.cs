using System;

#pragma warning disable S2342 // Enumeration types should comply with a naming convention
namespace FreeParkingSystem.Common.Models
{
    [Flags]
    public enum AccessLevel
    {
        /// <summary>
        /// Has full access
        /// </summary>
        Administrator = 16,
        /// <summary>
        /// has Editing access
        /// </summary>
        Moderator = 8,
        /// <summary>
        /// The owner of a position
        /// </summary>
        Owner = 4,
        /// <summary>
        /// Simple member
        /// </summary>
        Member = 2,
        /// <summary>
        /// Anonymous access
        /// </summary>
        Anonymous = 1
    }

    public enum PhoneNumberType
    {
        /// <summary>
        /// Local (to the country) number
        /// </summary>
        Local,
        /// <summary>
        /// National Number
        /// </summary>
        National,
        /// <summary>
        /// Mobile - Cellphone number
        /// </summary>
        Mobile,
        /// <summary>
        /// Fax Number
        /// </summary>
        Fax,
        /// <summary>
        /// Free of charge number
        /// </summary>
        FreeOfCharge
    }
}
