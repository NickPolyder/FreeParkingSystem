using System;
using RabbitMQ.Client;

namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public interface IBrokerConnection : IDisposable
	{
		bool IsConnected { get; }

		IModel CreateModel();

		bool Connect();
	}
}