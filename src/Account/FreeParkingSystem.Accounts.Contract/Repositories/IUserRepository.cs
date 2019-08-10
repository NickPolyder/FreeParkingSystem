using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Contract.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		bool UserExists(string userName);

		User GetByUsername(string userName);
	}
}