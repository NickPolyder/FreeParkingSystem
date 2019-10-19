using System;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.Client
{
	public interface IClientAuthorizationService : IDisposable
	{
		Task<UserToken> AuthorizeAsync(CancellationToken cancellationToken = default);
	}
}