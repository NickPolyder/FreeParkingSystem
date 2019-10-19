using System.Linq;
using System.Security.Claims;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Common.Encryption
{
	public class UserTokenEncryptor : IEncrypt<UserToken>
	{
		private static string[] _claimTypesForEncryption = new[]
		{
			UserClaimTypes.Id.ToString(),
			System.Security.Claims.ClaimTypes.Email,
			System.Security.Claims.ClaimTypes.Role,

		};
		private readonly IEncrypt<string> _stringEncryptor;

		public UserTokenEncryptor(IEncrypt<string> stringEncryptor)
		{
			_stringEncryptor = stringEncryptor;
		}

		public UserToken Encrypt(UserToken input)
		{
			var claims = input.Claims.ToList();
			foreach (var claimType in _claimTypesForEncryption)
			{
				var indexOfClaim = claims.FindIndex(item => item.Type == claimType);
				if (indexOfClaim > -1)
				{
					var encryptedValue = _stringEncryptor.Encrypt(claims[indexOfClaim].Value);
					claims[indexOfClaim] = new Claim(claimType, encryptedValue);
				}
			}

			return new UserToken(input.Username, claims, input.Token);
		}

		public UserToken Decrypt(UserToken input)
		{
			var claims = input.Claims.ToList();
			foreach (var claimType in _claimTypesForEncryption)
			{
				var indexOfClaim = claims.FindIndex(item => item.Type == claimType);
				if (indexOfClaim > -1)
				{
					var encryptedValue = _stringEncryptor.Decrypt(claims[indexOfClaim].Value);
					claims[indexOfClaim] = new Claim(claimType, encryptedValue);
				}
			}

			return new UserToken(input.Username, claims, input.Token);
		}

	}
}