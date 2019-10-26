using System;

namespace FreeParkingSystem.Common.MessageBroker
{
	public interface IMessageBroker : IDisposable
	{
		void Publish<TMessage>(TMessage message) where TMessage : BaseMessageBrokerRequest;

		void Subscribe<TMessage, THandler>()
			where TMessage : BaseMessageBrokerRequest
			where THandler : IMessageBrokerHandler<TMessage>;

		bool Unsubscribe<TMessage, THandler>()
			where THandler : IMessageBrokerHandler<TMessage>
			where TMessage : BaseMessageBrokerRequest;
	}
}