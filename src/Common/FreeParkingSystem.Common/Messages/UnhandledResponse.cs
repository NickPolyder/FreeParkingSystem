using System;

namespace FreeParkingSystem.Common.Messages
{
	public class UnhandledResponse : BaseResponse
	{
		public Exception Exception { get; }
		public UnhandledResponse(Guid id, Exception exception) : base(id)
		{
			Exception = exception;
		}
	}
}