using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Common.Authorization;
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

		public static BaseResponse ToValidationResponse(this BaseRequest @this, ErrorException ex)
		{
			return new ValidationResponse(@this.Id, ex);
		}

		public static BaseResponse ToUnhandledResponse(this BaseRequest @this, Exception ex)
		{
			return new UnhandledResponse(@this.Id, ex);
		}

		public static BaseResponse ToUnauthenticatedResponse(this BaseRequest @this)
		{
			return new UnauthenticatedResponse(@this.Id);
		}

		public static BaseResponse ToUnauthorizedResponse(this BaseRequest @this, IEnumerable<Role> roles)
		{
			return new UnauthorizedResponse(@this.Id, roles.ToArray());
		}
	}
}