using System;
using FreeParkingSystem.Common.API.Options;
using FreeParkingSystem.Common.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FreeParkingSystem.Common.API.ExtensionMethods
{
	public static class AuthenticationExtensions
	{
		public static void AddJwtAuthentication(this IServiceCollection services, Action<JwtAuthenticationOptions> configureOptions)
		{
			var options = new JwtAuthenticationOptions();
			configureOptions(options);

			services.AddSingleton(options);
			services.AddSingleton<IAuthenticationServices,JwtAuthenticationServices>();

			services.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = options.RequireHttpsMetadata;
					x.SaveToken = options.SaveToken;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
						IssuerSigningKey = new SymmetricSecurityKey(options.Secret),
						ValidateIssuer = options.ValidateIssuer,
						ValidateAudience = options.ValidateAudience,
						ValidAudience = options.ValidAudience,
						ValidAudiences = options.ValidAudiences,
						ValidIssuer = options.ValidIssuer,
						ValidIssuers = options.ValidIssuers
					};
				});

			services.AddHttpContextAccessor();
			services.AddScoped<IUserContextAccessor, UserContextAccessor>();
		}
	}
}