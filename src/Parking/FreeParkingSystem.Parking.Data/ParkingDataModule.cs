using Autofac;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Data.Mappers;
using FreeParkingSystem.Parking.Data.Models;
using FreeParkingSystem.Parking.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Parking.Data
{
	public class ParkingDataModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonDataModule>();

			builder.RegisterType<ParkingDbContext>().AsSelf().As<DbContext>();

			builder.RegisterType<FavoriteMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingTypeMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeMapper>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteViewMapper>().AsImplementedInterfaces();

			builder.RegisterType<FavoriteRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingTypeRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteViewRepository>().AsImplementedInterfaces();
		}
	}
}