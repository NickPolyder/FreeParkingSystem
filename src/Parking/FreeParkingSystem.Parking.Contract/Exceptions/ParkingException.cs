using System;
using System.Runtime.Serialization;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Parking.Contract.Exceptions
{
	public class ParkingException : ErrorException
	{
		public ParkingException()
		{
		}

		public ParkingException(string message) : base(message)
		{
		}

		public ParkingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ParkingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}