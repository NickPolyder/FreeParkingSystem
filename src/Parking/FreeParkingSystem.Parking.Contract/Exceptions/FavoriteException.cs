using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Parking.Contract.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class FavoriteException : ErrorException
	{
		public FavoriteException()
		{
		}

		public FavoriteException(string message) : base(message)
		{
		}

		public FavoriteException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected FavoriteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}