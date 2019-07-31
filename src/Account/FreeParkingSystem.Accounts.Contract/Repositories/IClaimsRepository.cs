using System.Collections.Generic;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts.Contract.Repositories
{
	public interface IClaimsRepository: IRepository<UserClaim>
	{
		IEnumerable<UserClaim> GetClaimsByUser(int userId);

	}
}