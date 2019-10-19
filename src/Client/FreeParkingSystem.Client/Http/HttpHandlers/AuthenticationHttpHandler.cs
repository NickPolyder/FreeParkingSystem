using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Common.Client.Http.HttpHandlers
{
	public class AuthenticationHttpHandler : DelegatingHandler
	{
		private readonly IClientAuthorizationService _clientAuthorizationService;

		public AuthenticationHttpHandler(IClientAuthorizationService clientAuthorizationService,
			HttpMessageHandler innerHandler) : base(innerHandler)
		{
			_clientAuthorizationService = clientAuthorizationService;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{

			var currentUser = await _clientAuthorizationService.AuthorizeAsync(cancellationToken);

			if (!string.IsNullOrWhiteSpace(currentUser?.Token))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("BEARER", currentUser.Token);
			}

			return await base.SendAsync(request, cancellationToken);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_clientAuthorizationService.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}