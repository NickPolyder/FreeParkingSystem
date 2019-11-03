using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using MediatR;

namespace FreeParkingSystem.Common.Behaviors
{
	public class AuthorizeBehaviour<TRequest> : IPipelineBehavior<TRequest, BaseResponse> where TRequest : BaseRequest
	{
		private readonly IUserContextAccessor _userContextAccessor;

		public AuthorizeBehaviour(IUserContextAccessor userContextAccessor)
		{
			_userContextAccessor = userContextAccessor;
		}

		public Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<BaseResponse> next)
		{
			var userContext = _userContextAccessor.GetUserContext();
			var authorizedRoles = typeof(TRequest)
				.GetCustomAttributes<AuthorizeRequestAttribute>()
				.Select(attr => attr.ProtectedBy)
				.ToArray();
			var hasAuthorizeAttribute = authorizedRoles.Any();

			if (hasAuthorizeAttribute && !userContext.IsAuthenticated())
			{
				return request.ToUnauthenticatedResponse().AsTask();
			}

			if (hasAuthorizeAttribute && !authorizedRoles.Any(role => role.HasValue && userContext.HasRole(role.Value)))
			{
				var roles = authorizedRoles.Where(attr => attr.HasValue)
					.Select(attr => attr.Value);

				return request.ToUnauthorizedResponse(roles).AsTask();
			}

			return next();
		}
	}
}