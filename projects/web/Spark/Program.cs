
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.util.swagger;
using Spark.souchy.celebi.spark.models;
using static souchy.celebi.spark.services.CreatureModelService;

namespace Spark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            // Add services to the container.
            configureServices(services, configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
            });
            //services.AddControllers();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new ControllerNamingConvention());
            });
            configureAuthentication(services, configuration);
            services.AddCors();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseStaticFiles("/../Jolteon/dist");

            app.Run();
        }


        private static void configureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            // relates toappsettings.json
            services.Configure<CelebiModelsDatabaseSettings>(configuration.GetSection("CelebiModelsDatabase"));

            //services.Configure<CreatureModelDatabaseSettings>(
            //    builder.Configuration.GetSection("CreatureModelDatabase") // relates toappsettings.json
            //);
            services.AddSingleton<MongoService>();
            services.AddSingleton<MongoModelsService>();
            services.AddSingleton<MongoFightsService>();
            services.AddSingleton<MongoExtraService>();
            services.AddSingleton<CreatureModelService>();
        }

        private static void configureAuthentication(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options => configuration.Bind("JwtSettings", options))
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options => configuration.Bind("CookieSettings", options))
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["google:clientId"];
                    googleOptions.ClientSecret = configuration["google:clientSecret"];
                });
            //.AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //});
        }

    }
}