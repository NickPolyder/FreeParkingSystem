using Autofac;
using FreeParkingSystem.Parking.Data.DatabaseModels.Mappers;

namespace FreeParkingSystem.Parking.Data.DatabaseModels
{
	public class ParkingDatabaseModelsModule: Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			builder.RegisterType<FavoriteMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingTypeMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteViewMapper>().AsImplementedInterfaces();
		}
	}
}