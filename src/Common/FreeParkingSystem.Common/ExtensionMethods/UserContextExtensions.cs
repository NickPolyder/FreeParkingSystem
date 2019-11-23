using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class UserContextExtensions
	{
		public static int GetUserId(this IUserContext userContext)
		{
			if (userContext.IsAuthenticated())
				return userContext.UserToken.Get<int>(UserClaimTypes.Id);

			return -1;
		}
	}
}