namespace FreeParkingSystem.Accounts.Contract
{
	public interface IUserServices
	{
		User GetById(int id);
		User CreateUser(string userName, string password);

		void AddClaim(User user, string type,string value);

		void ChangeClaim(User user, string type, string changedValue);

		void RemoveClaim(User user, string type);

		User Login(string username, string password);

	}
}