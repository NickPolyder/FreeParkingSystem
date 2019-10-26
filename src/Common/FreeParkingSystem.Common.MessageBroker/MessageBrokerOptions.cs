using System.Collections.Generic;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class MessageBrokerOptions
	{
		public int RetryAttempts { get; set; }

		public string BrokerName { get; set; }
		public string QueueName { get; set; }
		public bool Durable { get; set; }
		public bool Exclusive { get; set; }
		public bool AutoDelete { get; set; }
		public Dictionary<string, object> Arguments { get; set; }

	}
}