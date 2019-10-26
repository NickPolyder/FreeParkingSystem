using FreeParkingSystem.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using Shouldly;
using Xunit;

namespace FreeParkingSystem.Common.MessageBroker.Tests
{
	public class MessageBrokerTests
	{
		[Theory,FixtureData]
		public void RabbitMqBrokerConnection_Connection_ShouldConnect(Mock<ILoggerFactory> loggingFactoryMock)
		{
			var connectionOptions = new ConnectionOptions
			{
				RetryAttempts = 2
			};
			var connectionFactory = new ConnectionFactory();
			var brokerConnection = new RabbitMqBrokerConnection(connectionFactory,loggingFactoryMock.Object, connectionOptions);
			brokerConnection.Connect().ShouldBe(true);
		}
	}
}