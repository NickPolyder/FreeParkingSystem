using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Accounts.Contract.Commands
{
	[AuthorizeRequest(Role.Admin)]
	public class CreateUserRequest : BaseRequest
	{
		public string UserName { get; }

		public string Password { get; }

		public string Email { get; }

		public Role Role { get; }

		public CreateUserRequest(string userName, string password, string email, Role role)
		{
			UserName = userName;
			Password = password;
			Email = email;
			Role = role;
		}
	}
}