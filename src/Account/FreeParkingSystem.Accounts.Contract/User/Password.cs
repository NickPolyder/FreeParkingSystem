using System;

namespace FreeParkingSystem.Accounts.Contract.User
{
	public struct Password : IEquatable<Password>
	{
		private readonly string _password;
		public bool IsEncrypted { get; }

		private Password(string password, bool isEncrypted)
		{
			_password = password;
			IsEncrypted = isEncrypted;
		}

		public bool Equals(Password other)
		{
			return (_password == null ||
					_password.Equals(other._password)) &&
				   IsEncrypted == other.IsEncrypted;
		}

		public override bool Equals(object obj)
		{
			return obj is Password other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = _password == null ? 0 : _password.GetHashCode();
				hashCode = (hashCode * 257) ^ IsEncrypted.GetHashCode();
				return hashCode;
			}
		}

		public static bool operator ==(Password left, Password right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Password left, Password right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return _password;
		}
	}
}