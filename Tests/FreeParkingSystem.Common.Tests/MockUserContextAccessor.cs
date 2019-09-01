using System;
using System.Security.Claims;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.Tests
{
	public class MockUserContextAccessor:IUserContextAccessor
	{
		public IUserContext GetUserContext()
		{
			return new DefaultUserContext(new UserToken
			{
				Token = Guid.NewGuid().ToString(),
				Username = Guid.NewGuid().ToString(),
				Claims = Array.Empty<Claim>()
			});
		}
	}
}