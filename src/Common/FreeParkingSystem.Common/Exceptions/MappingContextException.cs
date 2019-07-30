using System;
using System.Runtime.Serialization;

namespace FreeParkingSystem.Common
{
	public class MappingContextException : Exception
	{
		public MappingContextException()
		{
		}

		public MappingContextException(string message) : base(message)
		{
		}

		public MappingContextException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MappingContextException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}