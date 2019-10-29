using System;

namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public interface ISubscriptionBroker: IDisposable
	{
		void Subscribe<TMessage, THandler>()
			where TMessage : BaseMessageBrokerRequest
			where THandler : IMessageBrokerHandler<TMessage>;

		bool Unsubscribe<TMessage, THandler>()
			where TMessage : BaseMessageBrokerRequest
			where THandler : IMessageBrokerHandler<TMessage>;
	}
}