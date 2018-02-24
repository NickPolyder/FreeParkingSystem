using System;
using System.Runtime.Serialization;

namespace FreeParkingSystem.Common.Services.Models
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class ServiceResultException : Exception
    {
        public ServiceResultException()
        { }

        public ServiceResultException(string message, Exception innerException) : base(message, innerException)
        { }

        public ServiceResultException(string message) : base(message)
        { }

        public ServiceResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
