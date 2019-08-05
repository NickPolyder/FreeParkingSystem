using System;
using System.Linq;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Exceptions;
using FreeParkingSystem.Accounts.Contract.Repositories;

namespace FreeParkingSystem.Accounts
{
	public class UserServices : IUserServices
	{
		private readonly IUserRepository _userRepository;
		private readonly IClaimsRepository _claimsRepository;
		private readonly IPasswordManager _passwordManager;

		public UserServices(IUserRepository userRepository,
			IClaimsRepository claimsRepository,
			IPasswordManager passwordManager)
		{
			_userRepository = userRepository;
			_claimsRepository = claimsRepository;
			_passwordManager = passwordManager;
		}

		public User CreateUser(string userName, string password)
		{
			if (_userRepository.UserExists(userName))
				throw new UserException(Contract.Resources.Validations.User_AlreadyExists);

			var encryptedPassword = _passwordManager.Create(password);

			var user = new User
			{
				UserName = userName,
				Password = encryptedPassword,
			};
			return _userRepository.Add(user);
		}

		public void AddClaim(User user, string type, string value)
		{
			Validate(user, type, value);

			if (_claimsRepository.UserHasClaim(user.Id, type))
				throw new ClaimException(Contract.Resources.Validations.Claim_AlreadyExists);

			var userClaim = _claimsRepository.Add(new UserClaim
			{
				User = user,
				UserId = user.Id,
				Type = type,
				Value = value
			});
			var oldClaims = user.Claims.ToList();
			var index = oldClaims.FindIndex(oldClaim => oldClaim.UserId == user.Id && oldClaim.Type == type);
			if (index >= 0)
			{
				oldClaims[index] = userClaim;
				user.Claims = oldClaims;
			}
			else
			{
				user.Claims.Add(userClaim);
			}
		}


		public void ChangeClaim(User user, string type, string changedValue)
		{
			Validate(user, type, changedValue);

			var userClaim = _claimsRepository.GetClaimByType(user.Id, type);
			
			if(userClaim == null)
				throw new ClaimException(Contract.Resources.Validations.Claim_DoesNotExist);

			userClaim.Value = changedValue;

			var oldClaims = user.Claims.ToList();
			var index = oldClaims.FindIndex(oldClaim => oldClaim.UserId == user.Id && oldClaim.Type == type);
			if (index >= 0)
			{
				oldClaims[index] = userClaim;
			}
			else
			{
				user.Claims.Add(userClaim);
			}
		}

		public void RemoveClaim(User user, string type)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentNullException(nameof(type));

			var userClaim = _claimsRepository.GetClaimByType(user.Id, type);

			if (userClaim == null)
				throw new ClaimException(Contract.Resources.Validations.Claim_DoesNotExist);

			_claimsRepository.Delete(userClaim.Id);

			var oldClaims = user.Claims.ToList();
			var index = oldClaims.FindIndex(oldClaim => oldClaim.UserId == user.Id && oldClaim.Type == type);
			if (index >= 0)
			{
				oldClaims.RemoveAt(index);
			}
		}


		private static void Validate(User user, string type, string value)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			if (string.IsNullOrWhiteSpace(type))
				throw new ArgumentNullException(nameof(type));

			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentNullException(nameof(value));
		}
	}
}