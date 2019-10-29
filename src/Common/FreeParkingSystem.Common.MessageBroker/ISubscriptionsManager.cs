using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Common.MessageBroker
{
	public interface ISubscriptionsManager : IDisposable
	{
		bool IsEmpty { get; }

		void AddSubscription<TMessage, THandler>()
			where TMessage : BaseMessageBrokerRequest
			where THandler : IMessageBrokerHandler<TMessage>;

		bool RemoveSubscription<TMessage, THandler>()
			where THandler : IMessageBrokerHandler<TMessage>
			where TMessage : BaseMessageBrokerRequest;

		bool HasSubscriptions<TMessage>() where TMessage : BaseMessageBrokerRequest;
		bool HasSubscriptions(string messageName);
		Type GetMessageTypeByName(string messageName);
		void Clear();
		IEnumerable<Type> GetHandlers<TMessage>() where TMessage : BaseMessageBrokerRequest;
		IEnumerable<Type> GetHandlers(string messageName);
		string GetMessageName<TMessage>();
	}
}