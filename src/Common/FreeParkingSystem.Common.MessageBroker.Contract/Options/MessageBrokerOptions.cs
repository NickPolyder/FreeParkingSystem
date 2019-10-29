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
		public Dictionary<string, object> Arguments { get; }

		internal MessageBrokerOptions(int retryAttempts, 
			string exchangeName,
			string queueName,
			bool isDurable, 
			bool isExclusive,
			bool isAutoDelete,
			Dictionary<string, object> arguments)
		{
			RetryAttempts = retryAttempts;
			ExchangeName = exchangeName;
			QueueName = queueName;
			IsDurable = isDurable;
			IsExclusive = isExclusive;
			IsAutoDelete = isAutoDelete;
			Arguments = arguments;
		}

	}
}