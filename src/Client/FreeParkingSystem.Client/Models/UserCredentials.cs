using FreeParkingSystem.Client.Http;
using FreeParkingSystem.Client.Http.Attributes;

namespace FreeParkingSystem.Client.Models
{
	[HttpRoute(HttpMethodConstants.POST, "/login")]
	public class UserCredentials
	{
		public string Username { get; set; }

		public string Password { get; set; }
	}
}