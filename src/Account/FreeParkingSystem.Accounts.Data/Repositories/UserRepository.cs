using System;
using System.Linq;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;

namespace FreeParkingSystem.Accounts.Data.Repositories
{
	public class UserRepository : BaseRepository<User, DbUser>, IUserRepository
	{
		public UserRepository(AccountsDbContext dbContext, IMap<DbUser, User> mapper) : base(dbContext, mapper)
		{
		}

		public bool UserExists(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
				throw new ArgumentNullException(nameof(userName));

			return Set.Any(user => user.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}