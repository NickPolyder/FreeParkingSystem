using Autofac;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.ExtensionMethods;
using FreeParkingSystem.Common.API.Options;
using FreeParkingSystem.Parking.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Parking.API
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
			var rabbitMqOptions = Configuration.GetSection("rabbitmq").Get<RabbitMqOptions>();

			services.AddRabbitMq((rabbitMqBuilder) => { rabbitMqBuilder.SetQueueName(rabbitMqOptions.QueueName); },
				(connection) =>
				{
					connection.HostName = rabbitMqOptions.HostName;
					connection.Port = rabbitMqOptions.Port;
				});

			var jwtOptions = Configuration.GetSection("jwt").Get<JwtAuthenticationOptions>();
			services.AddJwtAuthentication((options =>
			{
				options.Secret = jwtOptions.Secret;
				options.ValidAudience = jwtOptions.ValidAudience;
				options.ValidIssuer = jwtOptions.ValidIssuer;
				options.ExpiresAfter = jwtOptions.ExpiresAfter;
			}));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}
		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule<ParkingModule>();
			builder.RegisterInstance(new DbContextOptionsBuilder<ParkingDbContext>()
				.UseSqlServer(Configuration.GetConnectionString(nameof(ParkingDbContext)))
				.Options);

			var secretKey =
				Configuration.GetSection($"{nameof(EncryptionOptions)}:{nameof(EncryptionOptions.SecretKey)}").Get<byte[]>();

			builder.RegisterInstance(new EncryptionOptions(secretKey));
			
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
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
