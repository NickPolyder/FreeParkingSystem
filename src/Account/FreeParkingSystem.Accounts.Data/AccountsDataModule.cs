using Autofac;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Accounts.Data
{
	public class AccountsDataModule : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<CommonDataModule>();
			builder.RegisterType<AccountsDbContext>().AsSelf().As<DbContext>();

			builder.RegisterType<ClaimsMapper>().AsImplementedInterfaces();
			builder.RegisterType<UserMapper>().AsImplementedInterfaces();

			builder.RegisterType<ClaimsRepository>().AsImplementedInterfaces();
			builder.RegisterType<UserRepository>().AsImplementedInterfaces();
		}
	}
}
