using System.Collections.Generic;
using System.Security.Claims;

namespace FreeParkingSystem.Common.Authorization
{
	public interface IAuthenticationServices
	{
		UserToken CreateToken(string username, IEnumerable<Claim> claims);

		bool VerifyToken(string token, out UserToken userToken);
	}
}