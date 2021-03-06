﻿using Autofac;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker;
using FreeParkingSystem.Parking.Commands;
using FreeParkingSystem.Parking.Data;
using FreeParkingSystem.Parking.Mappers;
using FreeParkingSystem.Parking.Messages;
using FreeParkingSystem.Parking.Seeder;

namespace FreeParkingSystem.Parking
{
	public class ParkingModule : Module
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
			builder.RegisterType<ParkingTypeServices>().AsImplementedInterfaces();
			builder.RegisterType<ParkingSpotTypeServices>().AsImplementedInterfaces();


			builder.RegisterType<AddParkingSiteInputMapper>().AsImplementedInterfaces();
			builder.RegisterType<ChangeParkingSiteDetailsInputMapper>().AsImplementedInterfaces();
			builder.RegisterType<AddParkingSpotInputMapper>().AsImplementedInterfaces();
			builder.RegisterType<ChangeParkingSpotInputMapper>().AsImplementedInterfaces();

			builder.RegisterType<StartLeaseOnParkingSpotHandler>();
			builder.RegisterType<EndLeaseOnParkingSpotHandler>();
			builder.RegisterType<SubscriptionStarter>().As<IStartable>().SingleInstance();
		}
	}
}