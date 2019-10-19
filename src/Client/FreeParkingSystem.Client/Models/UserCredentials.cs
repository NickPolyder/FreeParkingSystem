using System.Net.Http;
using FreeParkingSystem.Common.Client.Http;
using FreeParkingSystem.Common.Client.Http.Attributes;

namespace FreeParkingSystem.Common.Client.Models
{
	[HttpRoute(HttpMethodConstants.POST, "/login")]
	public class UserCredentials
	{
		public string Username { get; set; }

		public string Password { get; set; }
	}
}