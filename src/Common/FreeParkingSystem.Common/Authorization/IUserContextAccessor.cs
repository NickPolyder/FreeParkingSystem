namespace FreeParkingSystem.Common.Authorization
{
	public interface IUserContextAccessor
	{
		IUserContext GetUserContext();
	}
}