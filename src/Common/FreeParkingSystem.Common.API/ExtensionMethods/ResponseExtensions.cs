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
					return new BadRequestObjectResult(validation.Error);
				case UnauthenticatedResponse unauthenticated:
					return new UnauthorizedObjectResult(unauthenticated.Error);
				case UnauthorizedResponse unauthorized:
					return new ObjectResult(unauthorized.Error)
					{
						StatusCode = (int)HttpStatusCode.Forbidden
					};
				case UnhandledResponse unhandled:
					return new ObjectResult(unhandled.Error)
					{
						StatusCode = (int)HttpStatusCode.InternalServerError
					};
				default:
					var error = new ErrorBuilder()
						.AddTitle(Resources.Validations.Unhandled_Title)
						.AddMessage(Resources.Validations.Unhandled_Message)
						.AddRequestId(response)
						.Build();
					return new ObjectResult(error)
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