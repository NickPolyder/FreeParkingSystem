using Autofac;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Parking.Data.DatabaseModels;
using FreeParkingSystem.Parking.Data.DatabaseModels.Mappers;
using FreeParkingSystem.Parking.Data.Models;
using FreeParkingSystem.Parking.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Parking.Data
{
	public class ParkingDataModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ParkingDbContext>().AsSelf().As<DbContext>();

			builder.RegisterModule<CommonDataModule>();
			builder.RegisterModule<ParkingDatabaseModelsModule>();

			builder.RegisterType<FavoriteRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingTypeRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeRepository>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSiteViewRepository>().AsImplementedInterfaces();
		}
	}
}