using Autofac;
using FreeParkingSystem.Parking.Data.Mappers;
using FreeParkingSystem.Parking.Data.Models;
using FreeParkingSystem.Parking.Data.Repositories;

namespace FreeParkingSystem.Parking.Data
{
	public class ParkingDataModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ParkingDbContext>();

			builder.RegisterType<FavoriteMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingTypeMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeMapper>().AsImplementedInterfaces();

			builder.RegisterType<FavoriteRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingTypeRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeRepository>().AsImplementedInterfaces();
		}
	}
}