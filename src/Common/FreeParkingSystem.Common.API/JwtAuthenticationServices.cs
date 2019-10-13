using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FreeParkingSystem.Common.API.Options;
using FreeParkingSystem.Common.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace FreeParkingSystem.Common.API
{
	public class JwtAuthenticationServices : IAuthenticationServices
	{
		private readonly JwtAuthenticationOptions _jwtAuthenticationOptions;
		private readonly IEncrypt<UserToken> _userTokenEncryptor;
		private JwtSecurityTokenHandler _jwtSecurityTokenHandler;
		private readonly SigningCredentials _signingCredentials;

		public JwtAuthenticationServices(JwtAuthenticationOptions jwtAuthenticationOptions,
			IEncrypt<UserToken> userTokenEncryptor)
		{
			_jwtAuthenticationOptions = jwtAuthenticationOptions;
			_userTokenEncryptor = userTokenEncryptor;
			_jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

			_signingCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtAuthenticationOptions.Secret), SecurityAlgorithms.HmacSha256Signature);
		}

		public UserToken CreateToken(string username, IEnumerable<Claim> claims)
		{
			var subjectClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username)
			};
			subjectClaims.AddRange(claims);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(subjectClaims),
				Expires = DateTime.UtcNow.Add(_jwtAuthenticationOptions.ExpiresAfter),
				Audience = _jwtAuthenticationOptions.ValidAudience,
				Issuer = _jwtAuthenticationOptions.ValidIssuer,
				SigningCredentials = _signingCredentials
			};

			var securityToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
			var token = _jwtSecurityTokenHandler.WriteToken(securityToken);

			var userToken = new UserToken
			{
				Username = username,
				Claims = subjectClaims,
				Token = token
			};

			return _userTokenEncryptor.Encrypt(userToken);
		}

		public bool VerifyToken(string token, out UserToken userToken)
		{
			var validationParameters = new TokenValidationParameters()
			{
				RequireExpirationTime = _jwtAuthenticationOptions.RequireExpirationTime,
				ValidateIssuer = _jwtAuthenticationOptions.ValidateIssuer,
				ValidateAudience = _jwtAuthenticationOptions.ValidateAudience,
				IssuerSigningKey = _signingCredentials.Key
			};

			var principal = _jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out var _);

			if (principal != null && principal.Identity.IsAuthenticated)
			{
				userToken = _userTokenEncryptor.Decrypt(new UserToken
				{
					Username = principal.Identity.Name,
					Claims = principal.Claims,
					Token = token
				});

				return true;
			}
			userToken = null;
			return false;
		}
	}
}