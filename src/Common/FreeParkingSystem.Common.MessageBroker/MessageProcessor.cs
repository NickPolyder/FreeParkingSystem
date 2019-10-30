using System;
using Autofac;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class MessageProcessor : IMessageProcessor
	{
		private const string AutofacScopeName = "RabbitMq_Message_Broker";
		private readonly ILifetimeScope _lifeTimeScope;
		private readonly ISubscriptionsManager _subscriptionsManager;
		private readonly IMessageConverter _messageConverter;

		public MessageProcessor(ILifetimeScope lifeTimeScope,
			ISubscriptionsManager subscriptionsManager,
			IMessageConverter messageConverter)
		{
			_lifeTimeScope = lifeTimeScope;
			_subscriptionsManager = subscriptionsManager;
			_messageConverter = messageConverter;
		}

		public void Process(Message message)
		{
			if (!_subscriptionsManager.HasSubscriptions(message.Name)) return;

			var messageType = _subscriptionsManager.GetMessageTypeByName(message.Name);
			var serializedMessage = _messageConverter.Convert(message.Body, messageType);

			var handleMethod = typeof(IMessageBrokerHandler<>).MakeGenericType(messageType).GetMethod("Handle");

			using (var scope = _lifeTimeScope.BeginLifetimeScope(AutofacScopeName))
			{
				var subscriptions = _subscriptionsManager.GetHandlers(message.Name);

				foreach (var subscription in subscriptions)
				{
					var handler = scope.ResolveOptional(subscription);
					if (handler == null) continue;

					handleMethod.Invoke(handler, new[] { serializedMessage });

				}
			}
		}
	}
}