using Autofac;
using RabbitMQ.Client;

namespace FreeParkingSystem.Common.MessageBroker
{
	public class MessageBrokerModule: Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<RabbitMqBrokerConnection>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<SubscriptionsManager>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<MessageConverter>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<MessageProcessor>().AsImplementedInterfaces().SingleInstance();
		
			builder.RegisterType<RabbitMqPublishBroker>().AsImplementedInterfaces().SingleInstance();

			builder.RegisterType<RabbitMqSubscriptionsBroker>().AsImplementedInterfaces().SingleInstance();
		}
	}
}