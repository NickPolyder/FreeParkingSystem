using System;

namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public interface IPublishBroker : IDisposable
	{
		void Publish<TMessage>(TMessage message) where TMessage : BaseMessageBrokerRequest;
	}
}