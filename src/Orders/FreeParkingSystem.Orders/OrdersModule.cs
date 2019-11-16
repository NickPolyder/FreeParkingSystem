using Autofac;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker;
using FreeParkingSystem.Orders.Commands;
using FreeParkingSystem.Orders.Data;
using FreeParkingSystem.Orders.Mappers;

namespace FreeParkingSystem.Orders
{
	public class OrdersModule: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonModule>();
			builder.RegisterModule<OrdersDataModule>();
			builder.RegisterModule<MessageBrokerModule>();

			builder.AddAssemblyForMediatR<StartLeaseHandler>();

			builder.RegisterType<OrderServices>().AsImplementedInterfaces();

			builder.RegisterType<StartLeaseInputMapper>().AsImplementedInterfaces();
		}
	}
}