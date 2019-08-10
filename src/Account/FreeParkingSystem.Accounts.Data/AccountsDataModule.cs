using Autofac;
using Autofac.Core;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;

namespace FreeParkingSystem.Accounts.Data
{
	public class AccountsDataModule : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AccountsDbContext>();

			builder.RegisterType<ClaimsMapper>().AsImplementedInterfaces();
			builder.RegisterType<UserMapper>().AsImplementedInterfaces();

			builder.RegisterType<ClaimsRepository>().AsImplementedInterfaces();
			builder.RegisterType<UserRepository>().AsImplementedInterfaces();
		}
	}
}
