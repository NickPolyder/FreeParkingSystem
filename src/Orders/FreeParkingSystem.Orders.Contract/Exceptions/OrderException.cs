using System;
using System.Runtime.Serialization;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Orders.Contract.Exceptions
{
	public class OrderException : ErrorException
	{
		public OrderException()
		{
		}

		public OrderException(string message) : base(message)
		{
		}

		public OrderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected OrderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}