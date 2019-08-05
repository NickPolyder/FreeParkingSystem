using System;
using System.Collections.Generic;
using System.Linq;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Accounts.Data.Repositories
{
	public class ClaimsRepository : BaseRepository<UserClaim,DbClaims>, IClaimsRepository
	{
		public ClaimsRepository(AccountsDbContext dbContext, IMap<DbClaims, UserClaim> claimsMapper):base(dbContext,claimsMapper)
		{
		}
		
		public IEnumerable<UserClaim> GetClaimsByUser(int userId)
		{
			var dbClaims = Set.Where(claim => claim.UserId == userId).ToArray();

			return Mapper.Map(dbClaims);
		}

		public bool UserHasClaim(int userId, string type)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentNullException(nameof(type));

			return Set.Any(claim => claim.UserId == userId && claim.ClaimType.Equals(type));
		}

		public UserClaim GetClaimByType(int userId, string type)
		{
			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentNullException(nameof(type));

			var claimItem = Set.SingleOrDefault(claim => claim.UserId == userId && claim.ClaimType.Equals(type));

			return Mapper.Map(claimItem);
		}
	}
}