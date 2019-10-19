using FreeParkingSystem.Common.Authorization;
using Microsoft.AspNetCore.Http;

namespace FreeParkingSystem.Common.API
{
	public class HttpUserContext : IUserContext
	{
		private readonly HttpContext _context;

		public UserToken UserToken
		{
			get
			{
				if (IsAuthenticated())
				{
					return new UserToken(_context.User.Identity.Name, _context.User.Claims);
				}

				return UserToken.Empty;

			}
		}

		public HttpUserContext(HttpContext context)
		{
			_context = context;
		}
		public bool HasRole(Role role)
		{
			if (!IsAuthenticated())
				return false;

			var roleAsString = role.ToString();
			return _context.User.IsInRole(roleAsString);
		}

		public bool IsAuthenticated()
		{
			return _context?.User?.Identity?.IsAuthenticated ?? false;
		}
	}
}