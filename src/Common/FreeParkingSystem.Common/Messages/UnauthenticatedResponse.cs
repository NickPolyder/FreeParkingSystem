using System;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Common.Messages
{
	public class UnauthenticatedResponse: BaseResponse
	{
		public Error Error { get; }
		public UnauthenticatedResponse(Guid requestId) : base(requestId)
		{
			Error = new ErrorBuilder()
				.AddTitle(Resources.Validations.Unauthenticated_Title)
				.AddMessage(Resources.Validations.Unauthenticated_Message)
				.AddRequestId(this)
				.Build();
			
		}
	}
}