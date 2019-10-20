using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Client.Accounts
{
	public interface IAccountClient : IClient
	{
		Task CreateUser(FreeParkingSystem.Accounts.Contract.Dtos.CreateUserInput createUser, CancellationToken cts = default);
	}
}