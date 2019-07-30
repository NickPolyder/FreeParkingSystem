using Autofac;
using Autofac.Core;
using FreeParkingSystem.Accounts.Data.Models;

namespace FreeParkingSystem.Accounts.Data
{
	public class AccountsDataModule : Autofac.Module
	{

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AccountsDbContext>();
		}
	}
}