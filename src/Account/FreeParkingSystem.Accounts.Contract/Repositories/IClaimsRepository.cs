using System.Collections.Generic;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Contract.Repositories
{
	public interface IClaimsRepository: IRepository<UserClaim>
	{
		IEnumerable<UserClaim> GetClaimsByUser(int userId);

		bool UserHasClaim(int userId, string type);

		UserClaim GetClaimByType(int userId, string type);
	}
}