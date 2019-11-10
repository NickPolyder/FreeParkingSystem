using System;

namespace FreeParkingSystem.Common.Messages
{
	public class NotFoundResponse : ValidationResponse
	{
		public NotFoundResponse(Guid requestId, ErrorException errorException) : base(requestId, errorException)
		{
		}
	}
}