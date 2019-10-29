
namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public class Message
	{
		public string Name { get; }
		public byte[] Body { get; }

		public Message(string name, byte[] body)
		{
			Name = name;
			Body = body;
		}

	}
}
