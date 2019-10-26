using System;

namespace FreeParkingSystem.Common.MessageBroker
{
	public abstract class BaseMessageBrokerRequest
	{
		protected BaseMessageBrokerRequest() : this(Guid.NewGuid(), DateTime.UtcNow)
		{ }

		protected BaseMessageBrokerRequest(Guid id, DateTime createDate)
		{
			Id = id;
			CreationDate = createDate;
		}

		public Guid Id { get; private set; }

		public DateTime CreationDate { get; private set; }
	}
}