using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract.Dtos;
using FreeParkingSystem.Client.Http;
using FreeParkingSystem.Client.Infrastructure;
using FreeParkingSystem.Client.Models;

namespace FreeParkingSystem.Client.Accounts
{
	public class AccountClient : IAccountClient
	{
		private readonly IAppDomainAccessor _appDomainAccessor;
		private readonly IHttpService _httpService;

		public AccountClient(IAppDomainAccessor appDomainAccessor, IHttpService httpService)
		{
			_appDomainAccessor = appDomainAccessor;
			_httpService = httpService;
		}
		public Task LoginAsync(UserCredentials credentials, CancellationToken cts = default)
		{
			throw new System.NotImplementedException();
		}

		public Task LogoutAsync(CancellationToken cts = default)
		{
			throw new System.NotImplementedException();
		}

		public Task CreateUser(CreateUserInput createUser, CancellationToken cts = default)
		{
			throw new System.NotImplementedException();
		}
	}
}