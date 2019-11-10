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
			return new SuccessResponse<TData>(@this.RequestId, data);
		}

		public static BaseResponse ToSuccessResponse(this BaseRequest @this)
		{
			return new SuccessResponse(@this.RequestId);
		}

		public static BaseResponse ToCreatedResponse<TData>(this BaseRequest @this, int createdId, TData data)
		{
			return new CreatedResponse<TData>(@this.RequestId, createdId, data);
		}

		public static BaseResponse ToCreatedResponse(this BaseRequest @this, int createdId)
		{
			return new CreatedResponse(@this.RequestId, createdId);
		}

		public static BaseResponse ToValidationResponse(this BaseRequest @this, ErrorException ex)
		{
			return new ValidationResponse(@this.RequestId, ex);
		}

		public static BaseResponse ToUnhandledResponse(this BaseRequest @this, Exception ex)
		{
			return new UnhandledResponse(@this.RequestId, ex);
		}

		public static BaseResponse ToUnauthenticatedResponse(this BaseRequest @this)
		{
			return new UnauthenticatedResponse(@this.RequestId);
		}

		public static BaseResponse ToUnauthorizedResponse(this BaseRequest @this, IEnumerable<Role> roles)
		{
			return new UnauthorizedResponse(@this.RequestId, roles.ToArray());
		}

		public static BaseResponse ToNotFoundResponse(this BaseRequest @this, ErrorException ex)
		{
			return new NotFoundResponse(@this.RequestId, ex);
		}
	}
}