using System;
using Autofac;
using FreeParkingSystem.Common.MessageBroker.Contract.Options;
using RabbitMQ.Client;

namespace FreeParkingSystem.Common.MessageBroker
{
	public static class ModuleExtensions
	{
		public static ContainerBuilder AddRabbitMqOptions(this ContainerBuilder containerBuilder,
			Action<RabbitMqOptionsBuilder> configureAction, Action<ConnectionFactory> configureConnectionFactory = null)
		{
			if(configureAction == null)
				throw new ArgumentNullException(nameof(configureAction));

			var optionsBuilder = new RabbitMqOptionsBuilder();
			configureAction.Invoke(optionsBuilder);

			var connectionFactory = new ConnectionFactory();
			configureConnectionFactory?.Invoke(connectionFactory);

			containerBuilder.RegisterInstance(connectionFactory).AsImplementedInterfaces();
			containerBuilder.RegisterInstance(optionsBuilder.Build());

			return containerBuilder;
		}
	}
}