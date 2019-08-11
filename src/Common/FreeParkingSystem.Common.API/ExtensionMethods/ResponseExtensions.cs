using System.Net;
using FreeParkingSystem.Common.Messages;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Common.API.ExtensionMethods
{
	public static class ResponseExtensions
	{

		public static IActionResult ToActionResult(this BaseResponse response)
		{
			switch (response)
			{
				case SuccessResponse _:
					return new OkResult();
				case ValidationResponse validation:
					return new BadRequestObjectResult(validation.ValidationException.Message);
				case UnauthenticatedResponse _:
					return new UnauthorizedResult();
				case UnauthorizedResponse unauthorized:
					return new ObjectResult(Resources.Validations.User_Unauthorized)
					{
						StatusCode = (int)HttpStatusCode.Forbidden
					};
				case UnhandledResponse unhandled:
					return new ObjectResult(unhandled.Exception.Message)
					{
						StatusCode = (int)HttpStatusCode.InternalServerError
					};
				default:
					return new ObjectResult(null)
					{
						StatusCode = (int)HttpStatusCode.InternalServerError
					};
			}
		}

		public static IActionResult ToActionResult<TData>(this BaseResponse response)
		{
			switch (response)
			{
				case SuccessResponse<TData> dataResponse:
					return new OkObjectResult(dataResponse.Data);
				default:
					return ToActionResult(response);
			}
		}
	}
}