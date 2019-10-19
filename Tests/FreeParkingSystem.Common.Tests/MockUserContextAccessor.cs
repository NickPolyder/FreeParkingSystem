using System;
using System.Security.Claims;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.Tests
{
	public class MockUserContextAccessor:IUserContextAccessor
	{
		public IUserContext GetUserContext()
		{
			return new DefaultUserContext(new UserToken(
				Guid.NewGuid().ToString(),
				Array.Empty<Claim>(),
				Guid.NewGuid().ToString()));
		}
	}
}