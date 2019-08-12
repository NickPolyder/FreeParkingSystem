using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FreeParkingSystem.Common
{
	[ExcludeFromCodeCoverage]
	public class ErrorException : Exception
	{
		public ErrorException()
		{
		}

		public ErrorException(string message) : base(message)
		{
		}

		public ErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}