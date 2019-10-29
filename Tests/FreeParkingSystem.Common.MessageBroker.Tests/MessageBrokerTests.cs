using Autofac;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.MessageBroker.Contract.Options;
using FreeParkingSystem.Common.Tests.Logging;
using RabbitMQ.Client;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace FreeParkingSystem.Common.MessageBroker.Tests
{
	public class MessageBrokerTests
	{
		private const string TestingSequence = "Testing_Sequence";
		private const string TestingQueue = "Testing_Queue";
		private const int RetryAttempts = 2;
		private readonly ITestOutputHelper _testOutputHelper;

		public MessageBrokerTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void RabbitMqBrokerConnection_Connection_ShouldConnect()
		{
			// Arrange
			var loggingFactory = new MockLoggerFactory(_testOutputHelper);
			var connectionOptions = new RabbitMqOptionsBuilder()
				.SetRetryAttempts(RetryAttempts)
				.SetExchangeName(TestingSequence)
				.SetQueueName(TestingQueue)
				.Build();

			var connectionFactory = new ConnectionFactory();

			var sut = new RabbitMqBrokerConnection(connectionFactory, loggingFactory, connectionOptions);

			// Act
			var result = sut.Connect();

			// Assert
			result.ShouldBe(true);

			sut.Dispose();
		}

		[Fact]
		public void CreateModel()
		{
			// Arrange
			var loggingFactory = new MockLoggerFactory(_testOutputHelper);
			var connectionOptions = new RabbitMqOptionsBuilder()
				.SetRetryAttempts(RetryAttempts)
				.SetExchangeName(TestingSequence)
				.SetQueueName(TestingQueue)
				.Build();


			var connectionFactory = new ConnectionFactory();

			var sut = new RabbitMqBrokerConnection(connectionFactory, loggingFactory, connectionOptions);

			// Act
			var result = sut.CreateModel();

			// Assert
			result.IsOpen.ShouldBe(true);

			result.Close();
			result.Dispose();

			sut.Dispose();
		}

		[Fact]
		public void Publish()
		{
			// Arrange
			var containerBuilder = new Autofac.ContainerBuilder();
			containerBuilder.AddRabbitMqOptions((builder) =>
			{
				builder.SetRetryAttempts(RetryAttempts)
					.SetAutoDelete(true)
					.SetExchangeName(TestingSequence)
					.SetQueueName(TestingQueue);
			});

			containerBuilder.RegisterInstance(new MockLoggerFactory(_testOutputHelper)).AsImplementedInterfaces();
			containerBuilder.RegisterType<RabbitMqBrokerConnection>().AsImplementedInterfaces();
			containerBuilder.RegisterInstance(new SubscriptionsManager()).AsImplementedInterfaces();
			
			containerBuilder.RegisterType<RabbitMqPublishBroker>().AsImplementedInterfaces();

			using (var container = containerBuilder.Build())
			using (var sut = container.Resolve<IPublishBroker>())
			{
				// Act
				sut.Publish(new HelloWorldMessage("Hello World"));

				// Assert

				sut.Dispose();
				container.Dispose();
			}
		}

		[Fact]
		public void Consume()
		{
			// Arrange
			var helloWorldHandler = new HelloWorldHandler(_testOutputHelper);

			var containerBuilder = new Autofac.ContainerBuilder();

			containerBuilder.AddRabbitMqOptions((builder) =>
			{
				builder.SetRetryAttempts(RetryAttempts)
					.SetAutoDelete(true)
					.SetExchangeName(TestingSequence)
					.SetQueueName(TestingQueue);
			});

			containerBuilder.RegisterInstance(new MockLoggerFactory(_testOutputHelper)).AsImplementedInterfaces();
			containerBuilder.RegisterType<RabbitMqBrokerConnection>().AsImplementedInterfaces();
			containerBuilder.RegisterInstance(new SubscriptionsManager()).AsImplementedInterfaces();
			containerBuilder.RegisterInstance(new MessageConverter()).AsImplementedInterfaces();
			containerBuilder.RegisterType<MessageProcessor>().AsImplementedInterfaces().SingleInstance();
			

			containerBuilder.RegisterType<RabbitMqPublishBroker>().AsImplementedInterfaces();

			containerBuilder.RegisterType<RabbitMqSubscriptionsBroker>().AsImplementedInterfaces();

			containerBuilder.RegisterInstance(helloWorldHandler);

			using (var container = containerBuilder.Build())
			using (var publisher = container.Resolve<IPublishBroker>())
			using (var sut = container.Resolve<ISubscriptionBroker>())
			{
				// Act

				publisher.Publish(new HelloWorldMessage("Hello World"));

				sut.Subscribe<HelloWorldMessage, HelloWorldHandler>();

				// Assert
				helloWorldHandler.ReceivingTask.Wait();
				helloWorldHandler.ReceivingTask.Result.ShouldBe(true);
			}
		}
	}
}