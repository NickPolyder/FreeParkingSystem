using System.Threading.Tasks;
using FreeParkingSystem.Common.Client.Models;
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
			var sut = new HttpService(() => messageHandler);

			// Act
			var result = await sut.SendAsync<UserLoginViewModel, UserLoginViewModel>(new UserLoginViewModel
			{
				Username = "admin",
				Password = "admin"
			});

			// Assert
			_helper.WriteLine($"HTTP Method: {messageHandler.CurrentRequest.Method} => {messageHandler.CurrentRequest.RequestUri}");

		}
	}
}