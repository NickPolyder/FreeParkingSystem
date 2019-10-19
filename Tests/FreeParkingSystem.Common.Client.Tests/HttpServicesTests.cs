using System.Net.Http;
using System.Threading.Tasks;
using FreeParkingSystem.Client.Http;
using FreeParkingSystem.Client.Models;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.Client.Tests
{
	public class HttpServicesTests
	{
		private readonly ITestOutputHelper _helper;

		public HttpServicesTests(ITestOutputHelper helper)
		{
			_helper = helper;
		}
		[Fact]
		public async Task Test()
		{
			// Arrange
			var messageHandler = new HttpMessageHandlerMock();
			var sut = new HttpService(new ParkingHttpClient(new HttpClient(messageHandler)));

			// Act
			var result = await sut.SendAsync<UserCredentials, UserCredentials>(new UserCredentials
			{
				Username = "admin",
				Password = "admin"
			});

			// Assert
			_helper.WriteLine($"HTTP Method: {messageHandler.CurrentRequest.Method} => {messageHandler.CurrentRequest.RequestUri}");

		}
	}
}