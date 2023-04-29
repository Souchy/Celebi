
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using souchy.celebi.spark.controllers.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.meta;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util.swagger;
using Spark.souchy.celebi.spark.models;
using static souchy.celebi.spark.services.models.CreatureModelService;

namespace Spark
{
    public static class Routes
    {
        public const string Models = "models/";
        public const string Fights = "fights/";
        public const string Meta = "meta/";
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
            });
            services.AddSwaggerGenNewtonsoftSupport();
            //services.AddControllers();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new ControllerNamingConvention());
            })
                .AddNewtonsoftJson();
            // Add services to the container.
            configureServices(services, configuration);
            configureAuthentication(services, configuration);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:9000", "http://localhost:9000")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            //services.AddFormatterMappings()
            //.AddJsonFormatters();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // cors must be after useRouting but before useAuthorization https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-7.0
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            //app.UseStaticFiles("/../Jolteon/dist");

            app.Run();
        }


        private static void configureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            // Mongo    
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings")); // relates to appsettings.json
            services.AddSingleton<MongoService>();
            services.AddSingleton<MongoModelsDbService>();
            services.AddSingleton<MongoFightsDbService>();
            // Meta
            services.AddSingleton<AccountService>();
            // Models
            services.AddSingleton<CreatureModelService>();
            services.AddSingleton<SpellModelController>();
            services.AddSingleton<StatusModelController>();
            // Fights
        }

        private static void configureAuthentication(IServiceCollection services, ConfigurationManager configuration)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
            //        options => configuration.Bind("JwtSettings", options))
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            //        options => configuration.Bind("CookieSettings", options))
            //    .AddGoogle(googleOptions =>
            //    {
            //        googleOptions.ClientId = configuration["google:clientId"];
            //        googleOptions.ClientSecret = configuration["google:clientSecret"];
            //    });
            ////.AddGoogle(options =>
            ////{
            ////    IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
            ////    options.ClientId = googleAuthNSection["ClientId"];
            ////    options.ClientSecret = googleAuthNSection["ClientSecret"];
            ////});

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "accounts.google.com",
                        ValidateAudience = false
                    };

                    options.MetadataAddress = "https://accounts.google.com/.well-known/openid-configuration";
                    options.TokenValidationParameters = tokenValidationParameters;
                })
            //    .AddGoogle(googleOptions =>
            //    {
            //        googleOptions.ClientId = configuration["google:clientId"];
            //        googleOptions.ClientSecret = configuration["google:clientSecret"];
            //    });
            ;
        }

    }
}