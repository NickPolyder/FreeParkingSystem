using System.Collections;
using System.Collections.Generic;
using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Common.ExtensionMethods;

namespace FreeParkingSystem.Accounts.Tests.Validators
{
	public class PasswordInvalidData: IEnumerable<object[]>
	{
		public IEnumerator<object[]> GetEnumerator()
		{
			yield return new object[]
			{
				new PasswordOptions(5,0,PasswordRequirements.None),
				new Password("012",false),
				Contract.Resources.Validations.PasswordValidation_MinimumCharacterRequired.WithArgs(5)
			};

			yield return new object[]
			{
				new PasswordOptions(0,3,PasswordRequirements.None),
				new Password("0123456",false),
				Contract.Resources.Validations.PasswordValidation_MaximumCharacterRequired.WithArgs(3)
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Numbers),
				new Password("password",false),
				Contract.Resources.Validations.PasswordValidation_NumbersRequired
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Capitals),
				new Password("password",false),
				Contract.Resources.Validations.PasswordValidation_CapitalsRequired
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Special),
				new Password("password",false),
				Contract.Resources.Validations.PasswordValidation_SpecialsRequired
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Numbers | PasswordRequirements.Capitals),
				new Password("password1",false),
				Contract.Resources.Validations.PasswordValidation_CapitalsRequired
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Special | PasswordRequirements.Capitals),
				new Password("p@ssword",false),
				Contract.Resources.Validations.PasswordValidation_CapitalsRequired
			};

			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Special | PasswordRequirements.Numbers),
				new Password("P@ssword",false),
				Contract.Resources.Validations.PasswordValidation_NumbersRequired
			};
			yield return new object[]
			{
				new PasswordOptions(0,0,PasswordRequirements.Capitals | PasswordRequirements.Numbers | PasswordRequirements.Special),
				new Password("pAssword1",false),
				Contract.Resources.Validations.PasswordValidation_SpecialsRequired
			};
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}