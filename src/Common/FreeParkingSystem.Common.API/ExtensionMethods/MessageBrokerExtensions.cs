using System;
using FreeParkingSystem.Common.MessageBroker.Contract.Options;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace FreeParkingSystem.Common.API.ExtensionMethods
{
	public static class MessageBrokerExtensions
	{
		public static void AddJwtAuthentication(this IServiceCollection services,
			Action<RabbitMqOptionsBuilder> configureAction, 
			Action<ConnectionFactory> configureConnectionFactory = null)
		{
			if (configureAction == null)
				throw new ArgumentNullException(nameof(configureAction));

			var optionsBuilder = new RabbitMqOptionsBuilder();
			configureAction.Invoke(optionsBuilder);

			var connectionFactory = new ConnectionFactory();
			configureConnectionFactory?.Invoke(connectionFactory);

			services.AddSingleton<IConnectionFactory>(connectionFactory);
			services.AddSingleton(optionsBuilder.Build());
		}
	}
}