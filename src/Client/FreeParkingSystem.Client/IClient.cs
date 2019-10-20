using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Client.Models;

namespace FreeParkingSystem.Client
{
	public interface IClient
	{
		Task LoginAsync(UserCredentials credentials, CancellationToken cts = default);

		Task LogoutAsync(CancellationToken cts = default);
	}
}