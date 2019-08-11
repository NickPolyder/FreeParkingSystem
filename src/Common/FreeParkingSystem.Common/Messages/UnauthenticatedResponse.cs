using System;

namespace FreeParkingSystem.Common.Messages
{
	public class UnauthenticatedResponse: BaseResponse
	{
		public UnauthenticatedResponse(Guid id) : base(id)
		{
		}
	}
}