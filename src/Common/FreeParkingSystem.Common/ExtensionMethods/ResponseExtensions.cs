using System;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class ResponseExtensions
	{
		public static BaseResponse ToSuccessResponse<TData>(this BaseRequest @this, TData data)
		{
			return new SuccessResponse<TData>(@this.Id, data);
		}

		public static BaseResponse ToSuccessResponse(this BaseRequest @this)
		{
			return new SuccessResponse(@this.Id);
		}

		public static BaseResponse ToValidationResponse(this BaseRequest @this, ValidationException ex)
		{
			return new ValidationResponse(@this.Id, ex);
		}

		public static BaseResponse ToUnhandledResponse(this BaseRequest @this, Exception ex)
		{
			return new UnhandledResponse(@this.Id, ex);
		}
	}
}