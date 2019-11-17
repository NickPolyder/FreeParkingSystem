using System.Collections.Generic;

namespace FreeParkingSystem.Common.MessageBroker.Contract.Options
{
	public class MessageBrokerOptions
	{
		public int RetryAttempts { get; }
		public string ExchangeName { get; }
		public string QueueName { get; }
		public bool IsDurable { get; }
		public bool IsExclusive { get; }
		public bool IsAutoDelete { get; }
		public Dictionary<string, object> ExchangeArguments { get; }

		public Dictionary<string, object> QueueArguments { get; }

		internal MessageBrokerOptions(int retryAttempts, 
			string exchangeName,
			string queueName,
			bool isDurable, 
			bool isExclusive,
			bool isAutoDelete,
			Dictionary<string, object> exchangeArguments, 
			Dictionary<string, object> queueArguments)
		{
			RetryAttempts = retryAttempts;
			ExchangeName = exchangeName;
			QueueName = queueName;
			IsDurable = isDurable;
			IsExclusive = isExclusive;
			IsAutoDelete = isAutoDelete;
			ExchangeArguments = exchangeArguments;
			QueueArguments = queueArguments;
		}

	}
}