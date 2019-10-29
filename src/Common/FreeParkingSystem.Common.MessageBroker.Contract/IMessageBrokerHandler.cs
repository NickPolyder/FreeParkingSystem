using System.Threading.Tasks;

namespace FreeParkingSystem.Common.MessageBroker.Contract
{
	public interface IMessageBrokerHandler<in TMessageBrokerRequest> where TMessageBrokerRequest : BaseMessageBrokerRequest
	{
		void Handle(TMessageBrokerRequest message);
	}
}