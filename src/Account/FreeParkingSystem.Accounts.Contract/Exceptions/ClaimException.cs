using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FreeParkingSystem.Accounts.Contract.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class ClaimException : Exception
	{
		public ClaimException()
		{
		}

		public ClaimException(string message) : base(message)
		{
		}

		public ClaimException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}