using System;

namespace FreeParkingSystem.Common.Messages
{

	public class SuccessResponse : BaseResponse
	{
		public SuccessResponse(Guid requestId) : base(requestId)
		{
		}
	}

	public class SuccessResponse<TData> : SuccessResponse
	{

		public TData Data { get; }

		public SuccessResponse(Guid requestId, TData data) : base(requestId)
		{
			Data = data;
		}

	}
}