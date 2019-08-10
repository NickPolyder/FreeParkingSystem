using System;

namespace FreeParkingSystem.Common.Messages
{
	public abstract class BaseResponse
	{
		public Guid Id { get; }

		protected BaseResponse(Guid id)
		{
			Id = id;
		}

	}
}