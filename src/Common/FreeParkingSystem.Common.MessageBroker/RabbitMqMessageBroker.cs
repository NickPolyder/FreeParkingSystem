using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class RabbitMqMessageBroker : IMessageBroker
	{
		private const string AutofacScopeName = "RabbitMq_Message_Broker";

		private readonly ILogger _logger;
		private readonly IBrokerConnection _brokerConnection;
		private readonly ISubscriptionsManager _subscriptionsManager;
		private readonly ILifetimeScope _autoFacLifetimeScope;
		private readonly MessageBrokerOptions _options;

		private IModel _channel;
		private bool _disposed;
		public RabbitMqMessageBroker(
			IBrokerConnection brokerConnection,
			ISubscriptionsManager subscriptionsManager,
			ILifetimeScope autoFacLifetimeScope,
			ILoggerFactory loggerFactory,
			MessageBrokerOptions options)
		{
			_brokerConnection = brokerConnection;
			_subscriptionsManager = subscriptionsManager;
			_autoFacLifetimeScope = autoFacLifetimeScope;
			_options = options;
			_logger = loggerFactory.CreateLogger<RabbitMqMessageBroker>();
			_channel = CreateChannel();
		}

		private IModel CreateChannel()
		{
			Connect();

			var channel = _brokerConnection.CreateModel();

			channel.ExchangeDeclare(exchange: _options.BrokerName,
				type: "direct");

			channel.QueueDeclare(queue: _options.QueueName,
				durable: _options.Durable,
				exclusive: _options.Exclusive,
				autoDelete: _options.AutoDelete,
				arguments: _options.Arguments);

			channel.CallbackException += (sender, ea) =>
			{
				_channel.Dispose();
				_channel = CreateChannel();
				StartBasicConsume();
			};

			return channel;
		}

		private void StartBasicConsume()
		{
			if (_channel == null) return;

			var consumer = new AsyncEventingBasicConsumer(_channel);

			consumer.Received += Consumer_Received;

			_channel.BasicConsume(
				queue: _options.QueueName,
				autoAck: false,
				consumer: consumer);
		}
		private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
		{
			var eventName = eventArgs.RoutingKey;
			var message = Encoding.UTF8.GetString(eventArgs.Body);

			try
			{
				await ProcessMessage(eventName, message);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, ex.Message);
			}

			// Even on exception we take the message off the queue.
			// in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
			// For more information see: https://www.rabbitmq.com/dlx.html
			_channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
		}

		private async Task ProcessMessage(string messageName, string message)
		{
			if (_subscriptionsManager.HasSubscriptions(messageName))
			{
				using (var scope = _autoFacLifetimeScope.BeginLifetimeScope(AutofacScopeName))
				{
					var subscriptions = _subscriptionsManager.GetHandlers(messageName);

					foreach (var subscription in subscriptions)
					{
						var handler = scope.ResolveOptional(subscription);
						if (handler == null) continue;
						var messageType = _subscriptionsManager.GetMessageTypeByName(messageName);
						var serializedMessage = JsonConvert.DeserializeObject(message, messageType);
						var handleMethod = typeof(IMessageBrokerHandler<>).MakeGenericType(messageType).GetMethod("Handle");

						await (Task)handleMethod.Invoke(handler, new[] { serializedMessage });

					}
				}
			}
		}

		public void Dispose()
		{
			if (_disposed) return;

			_disposed = true;
			_subscriptionsManager?.Dispose();
			_channel?.Dispose();
		}

		public void Publish<TMessage>(TMessage message) where TMessage : BaseMessageBrokerRequest
		{
			Connect();

			var policy = Policy.Handle<BrokerUnreachableException>()
				.Or<SocketException>()
				.WaitAndRetry(_options.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
				{
					_logger.LogWarning(ex, ex.Message);
				});

			var messageName = _subscriptionsManager.GetMessageName<TMessage>();
			
			_channel.ExchangeDeclare(exchange: _options.BrokerName, type: "direct");

			var serializedMessage = JsonConvert.SerializeObject(message);
			var body = Encoding.UTF8.GetBytes(serializedMessage);

			policy.Execute(() =>
			{
				var properties = _channel.CreateBasicProperties();
				properties.DeliveryMode = 2; // persistent
				
				_channel.BasicPublish(
					exchange: _options.BrokerName,
					routingKey: messageName,
					mandatory: true,
					basicProperties: properties,
					body: body);
			});
		}

		public void Subscribe<TMessage, THandler>() where TMessage : BaseMessageBrokerRequest where THandler : IMessageBrokerHandler<TMessage>
		{
			if (_subscriptionsManager.HasSubscriptions<TMessage>()) return;

			var messageName = _subscriptionsManager.GetMessageName<TMessage>();

			Connect();

			_channel.QueueBind(exchange: _options.BrokerName,
				queue: _options.QueueName,
				routingKey: messageName);

			_subscriptionsManager.AddSubscription<TMessage, THandler>();
		}

		public bool Unsubscribe<TMessage, THandler>() where TMessage : BaseMessageBrokerRequest where THandler : IMessageBrokerHandler<TMessage>
		{
			var result = _subscriptionsManager.RemoveSubscription<TMessage, THandler>();
			if (result)
			{
				var messageName = _subscriptionsManager.GetMessageName<TMessage>();
				Connect();

				_channel.QueueUnbind(exchange: _options.BrokerName,
					queue: _options.QueueName,
					routingKey: messageName);

				if (_subscriptionsManager.IsEmpty)
				{
					_channel.Close();
				}

			}

			return result;
		}

		private void Connect()
		{
			if (!_brokerConnection.IsConnected)
			{
				_brokerConnection.Connect();
			}
		}
	}
}