using System;

namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public interface IMessageConverter
	{
		object Convert(byte[] body, Type messageType);
	}
}