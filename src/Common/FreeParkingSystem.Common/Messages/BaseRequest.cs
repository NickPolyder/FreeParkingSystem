using System;
using MediatR;

namespace FreeParkingSystem.Common.Messages
{
	public abstract class BaseRequest : IRequest<BaseResponse> 
	{
		public Guid RequestId { get; }

		protected BaseRequest() : this(Guid.NewGuid())
		{

		}

		protected BaseRequest(Guid requestId)
		{
			RequestId = requestId;
		}
	}
}