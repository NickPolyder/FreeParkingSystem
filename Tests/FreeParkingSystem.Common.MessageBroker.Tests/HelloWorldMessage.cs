using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Common.MessageBroker.Tests
{
	public class HelloWorldMessage: BaseMessageBrokerRequest
	{
		public string Message { get; }

		public HelloWorldMessage(string message)
		{
			Message = message;
		}

	}
}