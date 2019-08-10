using System;

namespace FreeParkingSystem.Common.Messages
{
	public class ValidationResponse : BaseResponse
	{
		public ValidationException ValidationException { get; }
		public ValidationResponse(Guid id, ValidationException validationException) : base(id)
		{
			ValidationException = validationException;
		}
	}
}