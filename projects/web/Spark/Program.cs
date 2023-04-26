
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.util.swagger;
using Spark.souchy.celebi.spark.models;
using Spark.util.swagger;
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

            services.AddControllers();
            // Add services to the container.
            configureServices(services, configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("Spark", new OpenApiInfo { Title = "Spark", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
            });
            services.AddControllers(options =>
            {
                options.Conventions.Add(new ControllerNamingConvention());
            });

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
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
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