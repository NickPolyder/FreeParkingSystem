namespace FreeParkingSystem.Common.Authorization
{
	public interface IUserContext
	{
		UserToken UserToken { get; }

		bool HasRole(Role role);

		bool IsAuthenticated();
	}
}