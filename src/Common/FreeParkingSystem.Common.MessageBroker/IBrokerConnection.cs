using System;
using Polly.Retry;
using RabbitMQ.Client;

namespace FreeParkingSystem.Common.MessageBroker
{
	public interface IBrokerConnection : IDisposable
	{
		bool IsConnected { get; }

		IModel CreateModel();

		bool Connect();
	}
}