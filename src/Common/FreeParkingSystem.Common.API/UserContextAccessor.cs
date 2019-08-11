using FreeParkingSystem.Common.Authorization;
using Microsoft.AspNetCore.Http;

namespace FreeParkingSystem.Common.API
{
	public class UserContextAccessor : IUserContextAccessor
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public UserContextAccessor(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public IUserContext GetUserContext()
		{
			return new HttpUserContext(_contextAccessor.HttpContext);
		}
	}
}