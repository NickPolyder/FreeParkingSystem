using System;

namespace FreeParkingSystem.Common.Messages
{
	public abstract class BaseResponse
	{
		public Guid RequestId { get; }

		protected BaseResponse(Guid requestId)
		{
			RequestId = requestId;
		}

	}
}