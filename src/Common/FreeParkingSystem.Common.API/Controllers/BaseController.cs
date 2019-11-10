using System.Net;
using FreeParkingSystem.Common.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Common.API.Controllers
{
	public abstract class BaseController : ControllerBase
	{
		protected IActionResult ActionResult(BaseResponse response)
		{
			switch (response)
			{
				case CreatedResponse created:
					var idPath = Request.Path.Add(new PathString("/" + created.CreatedId)).Value;
					return new CreatedResult(idPath, null);
				case SuccessResponse _:
					return new OkResult();
				case NotFoundResponse notFoundError:
					return new ObjectResult(notFoundError.Error)
					{
						StatusCode = (int)HttpStatusCode.NotFound
					};
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

		protected IActionResult ActionResult<TData>(BaseResponse response)
		{
			switch (response)
			{
				case CreatedResponse<TData> created:
					var idPath = Request.Path.Add(new PathString("/" + created.CreatedId)).Value;
					return new CreatedResult(idPath, created.Data);
				case SuccessResponse<TData> dataResponse:
					return new OkObjectResult(dataResponse.Data);
				default:
					return ActionResult(response);
			}
		}
	}
}