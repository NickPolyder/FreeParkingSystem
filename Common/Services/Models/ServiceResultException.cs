using System;

namespace FreeParkingSystem.Common.Services.Models
{
    public class ServiceResultException : Exception
    {
        public ServiceResultException()
        { }

        public ServiceResultException(string message, Exception innerException = null) : base(message, innerException)
        { }
    }
}
