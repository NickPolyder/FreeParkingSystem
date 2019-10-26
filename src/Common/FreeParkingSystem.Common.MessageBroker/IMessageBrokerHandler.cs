using System.Threading.Tasks;

namespace FreeParkingSystem.Common.MessageBroker
{
	public interface IMessageBrokerHandler<in TMessageBrokerRequest> where TMessageBrokerRequest : BaseMessageBrokerRequest
	{
		Task Handle(TMessageBrokerRequest message);
	}
}