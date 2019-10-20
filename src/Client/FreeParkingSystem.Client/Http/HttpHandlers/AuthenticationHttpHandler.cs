using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Client.Http.HttpHandlers
{
	public class AuthenticationHttpHandler : DelegatingHandler
	{
		private readonly IUserContextAccessor _userContextAccessor;

		public AuthenticationHttpHandler(IUserContextAccessor userContextAccessor,
			HttpMessageHandler innerHandler) : base(innerHandler)
		{
			_userContextAccessor = userContextAccessor;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{

			var currentUser = _userContextAccessor.GetUserContext().UserToken;

			if (!string.IsNullOrWhiteSpace(currentUser?.Token))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("BEARER", currentUser.Token);
			}

			return base.SendAsync(request, cancellationToken);
		}

	}
}