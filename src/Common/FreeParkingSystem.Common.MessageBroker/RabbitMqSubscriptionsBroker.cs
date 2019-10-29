using System;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.MessageBroker.Contract.Options;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class RabbitMqSubscriptionsBroker: ISubscriptionBroker
	{
		private readonly ILogger _logger;
		private readonly IBrokerConnection _brokerConnection;
		private readonly ISubscriptionsManager _subscriptionsManager;
		private readonly IMessageProcessor _messageProcessor;

		private readonly MessageBrokerOptions _options;

		private IModel _consumerChannel;
		private bool _disposed;

		public RabbitMqSubscriptionsBroker(
			MessageBrokerOptions options,
			IBrokerConnection brokerConnection,
			ISubscriptionsManager subscriptionsManager,
			ILoggerFactory loggerFactory,
			IMessageProcessor messageProcessor)
		{
			_brokerConnection = brokerConnection;
			_subscriptionsManager = subscriptionsManager;
			_messageProcessor = messageProcessor;
			_options = options;
			_logger = loggerFactory.CreateLogger<RabbitMqPublishBroker>();
		}
		public void Subscribe<TMessage, THandler>() where TMessage : BaseMessageBrokerRequest where THandler : IMessageBrokerHandler<TMessage>
		{
			if (_subscriptionsManager.HasSubscriptions<TMessage>()) return;

			var messageName = _subscriptionsManager.GetMessageName<TMessage>();

			_brokerConnection.Connect();

			CreateConsumerChannel();

			_consumerChannel.QueueBind(exchange: _options.ExchangeName,
				queue: _options.QueueName,
				routingKey: messageName);

			_subscriptionsManager.AddSubscription<TMessage, THandler>();

			StartBasicConsume(_consumerChannel);
		}

		public bool Unsubscribe<TMessage, THandler>() where TMessage : BaseMessageBrokerRequest where THandler : IMessageBrokerHandler<TMessage>
		{
			var result = _subscriptionsManager.RemoveSubscription<TMessage, THandler>();
			if (result)
			{
				var messageName = _subscriptionsManager.GetMessageName<TMessage>();

				_brokerConnection.Connect();

				_consumerChannel.QueueUnbind(exchange: _options.ExchangeName,
					queue: _options.QueueName,
					routingKey: messageName);

				if (_subscriptionsManager.IsEmpty)
				{
					_consumerChannel.Close();
				}
			}

			return result;
		}

		public void Dispose()
		{
			if (_disposed) return;

			_disposed = true;
			_subscriptionsManager?.Dispose();
			_consumerChannel?.Dispose();
			_brokerConnection?.Dispose();
		}

		private void CreateConsumerChannel()
		{
			if (_consumerChannel != null && _consumerChannel.IsOpen)
				return;

			_brokerConnection.Connect();

			_consumerChannel = _brokerConnection.CreateModel();

			_consumerChannel.ExchangeDeclare(exchange: _options.ExchangeName,
				durable: _options.IsDurable,
				autoDelete: _options.IsAutoDelete,
				type: ExchangeType.Direct);

			_consumerChannel.QueueDeclare(queue: _options.QueueName,
				durable: _options.IsDurable,
				exclusive: _options.IsExclusive,
				autoDelete: _options.IsAutoDelete,
				arguments: _options.Arguments);

			_consumerChannel.CallbackException += (sender, ea) =>
			{
				_consumerChannel.Dispose();
				CreateConsumerChannel();
				StartBasicConsume(_consumerChannel);
			};
		}

		private void StartBasicConsume(IModel channel)
		{
			if (channel == null) return;

			var consumer = new EventingBasicConsumer(channel);

			consumer.Received += Consumer_Received;

			channel.BasicConsume(
				queue: _options.QueueName,
				autoAck: false,
				consumer: consumer);
		}
		private void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
		{
			try
			{
				_messageProcessor.Process(new Message(eventArgs.RoutingKey, eventArgs.Body));
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, ex.Message);
			}

			_consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
		}

	}
}