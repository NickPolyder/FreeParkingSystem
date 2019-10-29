namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public interface IMessageProcessor
	{
		void Process(Message message);
	}
}