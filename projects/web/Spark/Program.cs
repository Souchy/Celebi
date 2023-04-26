
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Spark.Models;
using Spark.Services;
using static Spark.Services.CreatureModelService;

namespace Spark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            configureServices(builder.Services, configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options => builder.Configuration.Bind("JwtSettings", options))
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options => builder.Configuration.Bind("CookieSettings", options))

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

    }
}