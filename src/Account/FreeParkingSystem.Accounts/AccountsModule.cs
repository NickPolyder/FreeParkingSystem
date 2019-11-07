using Autofac;
using FreeParkingSystem.Accounts.Commands;
using FreeParkingSystem.Accounts.Data;
using FreeParkingSystem.Accounts.Mappers;
using FreeParkingSystem.Accounts.Messages;
using FreeParkingSystem.Accounts.Seeder;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker;

namespace FreeParkingSystem.Accounts
{
	public class AccountsModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonModule>();
			builder.RegisterModule<AccountsDataModule>();
			builder.RegisterModule<MessageBrokerModule>();

			builder.AddAssemblyForMediatR<CreateUserHandler>();

			builder.RegisterType<PasswordValidator>().AsImplementedInterfaces();
			builder.RegisterType<PasswordEncryptor>().AsImplementedInterfaces();
			builder.RegisterType<PasswordHasher>().AsImplementedInterfaces();
			builder.RegisterType<PasswordManager>().AsImplementedInterfaces();
			builder.RegisterType<UserServices>().AsImplementedInterfaces();

			builder.RegisterType<SecurityClaimsMapper>().AsImplementedInterfaces();
			builder.RegisterType<UserLoginInputMapper>().AsImplementedInterfaces();
			builder.RegisterType<CreateUserInputMapper>().AsImplementedInterfaces();

			builder.RegisterType<UserCreatedParkingSiteHandler>();
			builder.RegisterType<UserDeletedParkingSiteHandler>();
			builder.RegisterType<SubscriptionStarter>().As<IStartable>().SingleInstance();
		}

	}
}