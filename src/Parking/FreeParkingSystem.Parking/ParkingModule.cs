using Autofac;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker;
using FreeParkingSystem.Parking.Commands;
using FreeParkingSystem.Parking.Data;
using FreeParkingSystem.Parking.Mappers;

namespace FreeParkingSystem.Parking
{
	public class ParkingModule:Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonModule>();
			builder.RegisterModule<ParkingDataModule>();
			builder.RegisterModule<MessageBrokerModule>();

			builder.AddAssemblyForMediatR<AddParkingSiteHandler>();

			builder.RegisterType<ParkingSiteServices>().AsImplementedInterfaces();
			builder.RegisterType<FavoriteServices>().AsImplementedInterfaces();
			builder.RegisterType<OrderApiServices>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotServices>().AsImplementedInterfaces();

			builder.RegisterType<AddParkingSiteInputMapper>().AsImplementedInterfaces();
			builder.RegisterType<ChangeParkingSiteDetailsInputMapper>().AsImplementedInterfaces();
		}
	}
}