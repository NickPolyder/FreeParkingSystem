using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Accounts.Contract.Queries
{
	public class UserLoginRequest:BaseRequest
	{
		public string Username { get; }

		public string Password { get; }

		public UserLoginRequest(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}