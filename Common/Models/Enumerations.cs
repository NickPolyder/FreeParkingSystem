using System;

namespace FreeParkingSystem.Common.Models
{
    [Flags]
    public enum AccessLevel
    {
        /// <summary>
        /// Has full access
        /// </summary>
        Administrator,
        /// <summary>
        /// has Editing access
        /// </summary>
        Moderator,
        /// <summary>
        /// The owner of a position
        /// </summary>
        Owner,
        /// <summary>
        /// Simple member
        /// </summary>
        Member,
        /// <summary>
        /// Anonymous access
        /// </summary>
        Anonymous
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
