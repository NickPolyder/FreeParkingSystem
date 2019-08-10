using Autofac;
using FreeParkingSystem.Accounts.Commands;
using FreeParkingSystem.Accounts.Data;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Accounts
{
	public class AccountsModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonModule>();
			builder.RegisterModule<AccountsDataModule>();

			builder.AddAssemblyForMediatR<CreateUserHandler>();
			builder.RegisterType<PasswordValidator>().AsImplementedInterfaces();
			builder.RegisterType<PasswordEncryptor>().AsImplementedInterfaces();
			builder.RegisterType<PasswordHasher>().AsImplementedInterfaces();
			builder.RegisterType<PasswordManager>().AsImplementedInterfaces();
			builder.RegisterType<UserServices>().AsImplementedInterfaces();

		}
	}
}