using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Data.Models;
using FreeParkingSystem.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FreeParkingSystem.Accounts
{
	public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }



		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging(options => options.AddConsole());
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule<AccountsModule>();
			builder.RegisterInstance(new DbContextOptionsBuilder<AccountsDbContext>()
					.UseSqlServer(Configuration.GetConnectionString(nameof(AccountsDbContext)))
					.Options);

			var secretKey =
				Configuration.GetSection($"{nameof(EncryptionOptions)}:{nameof(EncryptionOptions.SecretKey)}").Get<byte[]>();
			builder.RegisterInstance(
				new EncryptionOptions(secretKey));

			builder.RegisterInstance(GetPasswordOptions());


		}

		private PasswordOptions GetPasswordOptions()
		{
			var minimumCharacters =
				Configuration.GetValue<int>($"{nameof(PasswordOptions)}:{nameof(PasswordOptions.MinimumCharacters)}");

			var maximumCharacters =
				Configuration.GetValue<int>($"{nameof(PasswordOptions)}:{nameof(PasswordOptions.MaximumCharacters)}");

			var requirements =
				Configuration.GetValue<PasswordRequirements>($"{nameof(PasswordOptions)}:{nameof(PasswordOptions.Requirements)}");

			var allowedSpecialCharacters =
				Configuration.GetValue<string>($"{nameof(PasswordOptions)}:{nameof(PasswordOptions.AllowedSpecialCharacters)}");

			return new PasswordOptions(minimumCharacters, maximumCharacters, requirements, allowedSpecialCharacters);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
