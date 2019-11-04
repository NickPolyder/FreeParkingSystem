using System;

namespace FreeParkingSystem.Common.Messages
{

	public class CreatedResponse : BaseResponse
	{
		public int CreatedId { get; }
		public CreatedResponse(Guid requestId,int createdId) : base(requestId)
		{
			CreatedId = createdId;
		}
	}

	public class CreatedResponse<TData> : CreatedResponse
	{

		public TData Data { get; }

		public CreatedResponse(Guid requestId,int createdId, TData data) : base(requestId, createdId)
		{
			Data = data;
		}

	}
}