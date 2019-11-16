using Autofac;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Orders.Data.Mappers;
using FreeParkingSystem.Orders.Data.Models;
using FreeParkingSystem.Orders.Data.Repositories;
using FreeParkingSystem.Parking.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Orders.Data
{
	public class OrdersDataModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonDataModule>();
			builder.RegisterModule<ParkingDatabaseModelsModule>();

			builder.RegisterType<OrdersDbContext>().AsSelf().As<DbContext>();

			builder.RegisterType<OrderMapper>().AsImplementedInterfaces();
			builder.RegisterType<OrderViewMapper>().AsImplementedInterfaces();

			builder.RegisterType<OrdersRepository>().AsImplementedInterfaces();
			builder.RegisterType<OrderViewRepository>().AsImplementedInterfaces();
		}

	}
}