using System;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Client.Http;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Client
{
	public class DefaultClientAuthorizationService : IClientAuthorizationService
	{
		private readonly IHttpService _httpService;

		public DefaultClientAuthorizationService(IHttpService httpService)
		{
			_httpService = httpService;
		}
		public Task<UserToken> AuthorizeAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
		
		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}

	public class UserOptions
	{

	}
}