using System;

namespace FreeParkingSystem.Accounts.Contract.User
{
	[Flags]
	public enum PasswordRequirements : byte
	{
		None = 0b000,
		Special = 0b001,
		Numbers = 0b010,
		Capitals = 0b100
	}
}