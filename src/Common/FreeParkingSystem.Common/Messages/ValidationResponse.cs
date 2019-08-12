using System;

namespace FreeParkingSystem.Common.Messages
{
	public class ValidationResponse : BaseResponse
	{
		public Error Error { get; }
		public ErrorException ErrorException { get; }
		public ValidationResponse(Guid requestId, ErrorException errorException) : base(requestId)
		{
			ErrorException = errorException;
			Error = new ErrorBuilder()
				.AddTitle(Resources.Validations.Validation_Title)
				.AddMessage(ErrorException.Message)
				.AddException(ErrorException)
				.AddRequestId(this)
				.Build();

		}
	}
}