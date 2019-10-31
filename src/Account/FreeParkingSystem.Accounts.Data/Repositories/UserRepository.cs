using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Common.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Accounts.Data.Repositories
{
	public class UserRepository : BaseRepository<User, DbUser>, IUserRepository
	{
		public UserRepository(AccountsDbContext dbContext, IMap<DbUser, User> mapper) : base(dbContext, mapper)
		{
		}

		protected override IEnumerable<Expression<Func<DbUser, object>>> GetIncludes()
		{
			return new Expression<Func<DbUser, object>>[]
			{
				user => user.Claims,
			};
		}

		public bool UserExists(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
				throw new ArgumentNullException(nameof(userName));

			return Set.Any(user => user.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
		}

		public User GetByUsername(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
				return null;

			var user = Set.Include(x => x.Claims).FirstOrDefault(users => users.UserName.Equals(userName));

			return Mapper.Map(user);
		}
	}
}