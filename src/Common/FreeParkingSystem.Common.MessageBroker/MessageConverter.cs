using System;
using FreeParkingSystem.Common.MessageBroker.Contract;
using Newtonsoft.Json;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class MessageConverter : IMessageConverter
	{
		public object Convert(byte[] body, Type messageType)
		{
			var json = System.Text.Encoding.UTF8.GetString(body);

			return JsonConvert.DeserializeObject(json, messageType);
		}
	}
}