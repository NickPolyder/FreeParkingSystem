using System;
using System.Net.Sockets;
using System.Text;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.MessageBroker.Contract.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class RabbitMqPublishBroker : IPublishBroker
	{
		private readonly ILogger _logger;
		private readonly IBrokerConnection _brokerConnection;
		private readonly ISubscriptionsManager _subscriptionsManager;
		private readonly MessageBrokerOptions _options;

		private IModel _mainChannel;
		private bool _disposed;

		public RabbitMqPublishBroker(
			IBrokerConnection brokerConnection,
			ISubscriptionsManager subscriptionsManager,
			ILoggerFactory loggerFactory,
			MessageBrokerOptions options)
		{
			_brokerConnection = brokerConnection;
			_subscriptionsManager = subscriptionsManager;
			_options = options;
			_logger = loggerFactory.CreateLogger<RabbitMqPublishBroker>();
			CreateMainChannel();
		}

		public void Publish<TMessage>(TMessage message) where TMessage : BaseMessageBrokerRequest
		{
			var policy = Policy.Handle<BrokerUnreachableException>()
				.Or<SocketException>()
				.WaitAndRetry(_options.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
				{
					_logger.LogWarning(ex, ex.Message);
				});

			var messageName = _subscriptionsManager.GetMessageName<TMessage>();
			var serializedMessage = JsonConvert.SerializeObject(message);
			var body = Encoding.UTF8.GetBytes(serializedMessage);

			policy.Execute(() =>
			{
				_brokerConnection.Connect();

				ExchangeDeclare();

				_mainChannel.QueueBind(_options.QueueName, _options.ExchangeName,messageName);

				var properties = _mainChannel.CreateBasicProperties();
				properties.Persistent = true;
				
				_mainChannel.BasicPublish(
					exchange: _options.ExchangeName,
					routingKey: messageName,
					mandatory: true,
					basicProperties: properties,
					body: body);
			});
		}

		public void Dispose()
		{
			if (_disposed) return;

			_disposed = true;
			_subscriptionsManager?.Dispose();
			_mainChannel?.Dispose();
			_brokerConnection?.Dispose();
		}
		
		private void CreateMainChannel()
		{
			_brokerConnection.Connect();

			_mainChannel = _brokerConnection.CreateModel();

			ExchangeDeclare();

			_mainChannel.QueueDeclare(queue: _options.QueueName,
				durable: _options.IsDurable,
				exclusive: _options.IsExclusive,
				autoDelete: _options.IsAutoDelete,
				arguments: _options.Arguments);

			_mainChannel.CallbackException += (sender, ea) =>
			{
				_mainChannel.Dispose();
				CreateMainChannel();
			};
		}

		private void ExchangeDeclare()
		{
			_mainChannel.ExchangeDeclare(exchange: _options.ExchangeName,
				durable: _options.IsDurable,
				autoDelete: _options.IsAutoDelete,
				type: ExchangeType.Direct);
		}
	}
}