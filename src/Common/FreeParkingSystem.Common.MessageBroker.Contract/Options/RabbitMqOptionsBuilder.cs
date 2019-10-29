using System;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.MessageBroker.Contract.Options
{
	public class RabbitMqOptionsBuilder
	{
		public int RetryAttempts { get; private set; } = 2;
		public string ExchangeName { get; private set; } = "FreeParking_System";
		public string QueueName { get; private set; }
		public bool IsDurable { get; private set; } = true;
		public bool IsExclusive { get; private set; } = false;
		public bool IsAutoDelete { get; private set; } = false;
		public Dictionary<string, object> Arguments { get; private set; } = new Dictionary<string, object>();

		public RabbitMqOptionsBuilder SetRetryAttempts(int retryAttempts)
		{
			ValidateRetryAttempts(retryAttempts);

			RetryAttempts = retryAttempts;

			return this;
		}

		public RabbitMqOptionsBuilder SetExchangeName(string exchangeName)
		{
			ValidateExchangeName(exchangeName);

			ExchangeName = exchangeName;

			return this;
		}

		public RabbitMqOptionsBuilder SetQueueName(string queueName)
		{
			ValidateQueueName(queueName);

			QueueName = queueName;

			return this;
		}


		public RabbitMqOptionsBuilder SetDurable(bool isDurable)
		{
			IsDurable = isDurable;

			return this;
		}

		public RabbitMqOptionsBuilder SetExclusive(bool isExclusive)
		{
			IsExclusive = isExclusive;

			return this;
		}

		public RabbitMqOptionsBuilder SetAutoDelete(bool isAutoDelete)
		{
			IsAutoDelete = isAutoDelete;

			return this;
		}

		public RabbitMqOptionsBuilder SetArguments(Dictionary<string, object> arguments)
		{
			ValidateArguments(arguments);
			Arguments = arguments;

			return this;
		}

		public MessageBrokerOptions Build()
		{
			ValidateRetryAttempts(RetryAttempts);
			ValidateExchangeName(ExchangeName);
			ValidateQueueName(QueueName);
			ValidateArguments(Arguments);

			return new MessageBrokerOptions(RetryAttempts,
				ExchangeName,
				QueueName,
				IsDurable,
				IsExclusive,
				IsAutoDelete,
				Arguments);
		}
		

		private static void ValidateRetryAttempts(int retryAttempts)
		{
			if (retryAttempts < 0)
				throw new ArgumentOutOfRangeException(nameof(retryAttempts), retryAttempts, "Should be 0 or above");
		}

		private static void ValidateExchangeName(string exchangeName)
		{
			if (string.IsNullOrWhiteSpace(exchangeName))
				throw new ArgumentNullException(nameof(exchangeName));
		}
		private static void ValidateQueueName(string queueName)
		{
			if (string.IsNullOrWhiteSpace(queueName))
				throw new ArgumentNullException(nameof(queueName));
		}
		private static void ValidateArguments(Dictionary<string, object> arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));
		}
	}
}