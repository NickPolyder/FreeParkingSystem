using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Contract.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class UserException : ValidationException
	{
		public UserException()
		{
		}

		public UserException(string message) : base(message)
		{
		}

		public UserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}