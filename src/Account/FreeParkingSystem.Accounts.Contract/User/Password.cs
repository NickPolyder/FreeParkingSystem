using System;

namespace FreeParkingSystem.Accounts.Contract.User
{
	public struct Password : IEquatable<Password>
	{
		private readonly string _password;

		public string Salt { get; }
		public bool IsHashed { get; }
		public bool IsEncrypted { get; }

		public Password(string password) : this(password, false)
		{
		}

		public Password(string password, bool isHashed) : this(password, isHashed, false)
		{
		}

		public Password(string password, bool isHashed, bool isEncrypted) : this(password, string.Empty, isHashed, isEncrypted)
		{
		}

		public Password(string password, string salt) : this(password, salt, false)
		{
		}

		public Password(string password, string salt, bool isHashed) : this(password, salt, isHashed, false)
		{
		}

		public Password(string password, string salt, bool isHashed, bool isEncrypted)
		{
			_password = password;
			Salt = salt ?? string.Empty;
			IsHashed = isHashed;
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
			if (IsHashed || IsEncrypted)
				return _password;

			return string.Format("{1}{0}{1}", _password, Salt);
		}
	}
}