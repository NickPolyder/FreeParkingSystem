using System;

namespace FreeParkingSystem.Common.Messages
{

	public class SuccessResponse : BaseResponse
	{
		public SuccessResponse(Guid id) : base(id)
		{
		}
	}

	public class SuccessResponse<TData> : SuccessResponse
	{

		public TData Data { get; }

		public SuccessResponse(Guid id, TData data) : base(id)
		{
			Data = data;
		}

	}
}