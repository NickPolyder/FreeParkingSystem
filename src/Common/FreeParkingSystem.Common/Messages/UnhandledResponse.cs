using System;

namespace FreeParkingSystem.Common.Messages
{
	public class UnhandledResponse : BaseResponse
	{
		public Error Error { get; }
		public Exception Exception { get; }
		public UnhandledResponse(Guid requestId, Exception exception) : base(requestId)
		{
			Exception = exception;
			Error = new ErrorBuilder()
				.AddTitle(Resources.Validations.Unhandled_Title)
				.AddMessage(Exception.Message)
				.AddRequestId(this)
				.AddException(Exception)
				.Build();
		}
	}
}