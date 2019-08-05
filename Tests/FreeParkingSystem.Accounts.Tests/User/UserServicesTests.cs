using AutoFixture;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Repositories;
using FreeParkingSystem.Accounts.Data.Mappers;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Accounts.Data.Repositories;
using FreeParkingSystem.Accounts.Validators;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Encryption;
using FreeParkingSystem.Common.Hashing;
using Microsoft.EntityFrameworkCore;

namespace FreeParkingSystem.Accounts.Tests.User
{
	public class UserServicesTests
	{
		private static void ContainerSetup(IFixture fixture)
		{

			fixture.Build<IUserRepository>()
				.FromFactory(() => new UserRepository(GetDbContext(),new UserMapper(new ClaimsMapper())))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IClaimsRepository>()
				.FromFactory(() => new ClaimsRepository(GetDbContext(),new ClaimsMapper()))
				.ToCustomization()
				.Customize(fixture);

			fixture.Build<IPasswordManager>()
				.FromFactory(() =>
				{
					var options = new PasswordOptions(0, 1, PasswordRequirements.None);

					var passwordValidator = new PasswordValidator(options);

					var hashingPassword = new PasswordHasher(new ShaStringHasher(new ShaByteHasher()));

					var secretKey = new byte[32]
					{
						237, 201, 222, 52, 18, 152, 49, 135, 198, 143, 48, 247, 22, 185, 5, 216, 43, 6, 37, 243, 13, 52, 149, 119, 74, 104, 70, 130, 246, 76, 231, 147
					};

					var encryptPassword = new PasswordEncryptor(new AesStringEncryptor(new AesByteEncryptor(new EncryptionOptions(secretKey))));

					return new PasswordManager(passwordValidator, hashingPassword, encryptPassword);
				})
				.ToCustomization()
				.Customize(fixture);
		}

		private static AccountsDbContext GetDbContext()
		{
			var options = new DbContextOptionsBuilder<AccountsDbContext>()
				.UseInMemoryDatabase("InMemory")
				.Options;

			var dbContext = new AccountsDbContext(options);
			return dbContext;
		}
	}
}