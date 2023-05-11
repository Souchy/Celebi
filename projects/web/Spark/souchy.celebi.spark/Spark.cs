
using AspNetCore.Identity.Mongo.Model;
using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using souchy.celebi.spark.controllers.meta;
using souchy.celebi.spark.controllers.models;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.fights;
using souchy.celebi.spark.services.meta;
using souchy.celebi.spark.services.models;
using souchy.celebi.spark.util.swagger;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using souchy.celebi.spark.models.settings;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.OpenApi.Any;
using souchy.celebi.eevee.face.entity;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.effects;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.impl.objects.zones;
using System.Reflection;

namespace souchy.celebi.spark
{
    public static class Routes
    {
        public const string Models = "models/";
        public const string Fights = "fights/";
        public const string Meta = "meta/";
    }

    public class Spark
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;
            // Register types
            configureMongo();
            // services
            configureBuilder(services, configuration);
            // App
            configureApp(builder.Build());
        }

        private static void configureBuilder(IServiceCollection services, ConfigurationManager configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer(); // ??

            // swagger
            configureSwagger(services);

            // Add services to the container.
            configureServices(services, configuration);
            //configureAuthentication(services, configuration);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder
                        .WithOrigins("https://localhost:9000", "http://localhost:9000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });

            services.AddIdentityMongoDbProvider<Account, AccountRole>(identity =>
                {
                    //identity.coo
                    identity.Password.RequiredLength = 8;
                    identity.Password.RequireNonAlphanumeric = true;
                    identity.User.RequireUniqueEmail = true;
                    //identity.SignIn.RequireConfirmedEmail = true;
                    //identity.User.AllowedUserNameCharacters= new[] {}
                },
                mongo =>
                {
                    var settings = configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
                    if (settings == null) return; // for swashbuckle 'dotnet swagger' generator. secrets aren't included at that moment
                    mongo.ConnectionString = settings.ConnectionString + "/" + settings.MetaDB; 
                    mongo.UsersCollection = nameof(Account);
                }
            );
            services.ConfigureApplicationCookie(options =>
            {
                var settings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
                //configuration.Bind("CookieSettings", options);
                options.Cookie.Name = "squid";
                options.ExpireTimeSpan = TimeSpan.FromDays(14); //TimeSpan.FromSeconds(30);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                //options.Cookie.Expiration = TimeSpan.FromSeconds(30);

                // this happens when you try to access unauthorized resource, it redirects to login
                options.LoginPath = "/meta/auth/mammoth"; 
                options.LogoutPath = "/penguin";
                options.AccessDeniedPath = "/orangoutan";
                options.ReturnUrlParameter = "challenged";
                options.ClaimsIssuer = settings.Issuer;
                options.SlidingExpiration = true;
                
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = c =>
                    {
                        //c.HttpContext.Response.Redirect("https://localhost:9000/mammoth");
                        c.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //c.HttpContext.Response.Headers.Add("logout", "OnRedirectToLogin");
                        HttpResponseWritingExtensions.WriteAsync(c.Response, "logout");
                        c.HttpContext.SignOutAsync();
                        //c.HttpContext.User.
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = c =>
                    {
                        c.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //c.HttpContext.Response.Headers.Add("logout", "OnRedirectToAccessDenied");
                        HttpResponseWritingExtensions.WriteAsync(c.Response, "logout");
                        c.HttpContext.SignOutAsync();
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void configureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
                c.SchemaFilter<ClassEnumSchemaFilter>();
                //c.
                //c.MapType<IID>(() => new OpenApiSchema() { Type = "IID" });
                //c.MapType<IID>(() => new OpenApiSchema() { Type = "string" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new ControllerNamingConvention());
                
            }).AddNewtonsoftJson();
        }

        private static void configureApp(WebApplication app)
        {
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
            // Jwt 
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            // Mongo    
            services.Configure<MongoSettings>(configuration.GetSection(nameof(MongoSettings))); // relates to appsettings.json
            services.AddSingleton<MongoClientService>();
            services.AddSingleton<MongoModelsDbService>();
            services.AddSingleton<MongoFightsDbService>();
            services.AddSingleton<MongoMetaDbService>();
            services.AddSingleton<MongoI18NDbService>();

            // Meta
            services.AddSingleton<AccountService>();
            services.AddSingleton<CurrencyProductService>();
            services.AddSingleton<ModelProductService>();
            services.AddSingleton<ConsumableProductService>();
            // Models
            services.AddSingleton<SkinService>();
            services.AddSingleton<StringService>();
            services.AddSingleton<IDCounterService>();
            // Fights
            services.AddSingleton<FightService>();

            //services.AddTransient<ICreatureModel, CreatureModel>();
            //services.AddTransient<ISpellModel, SpellModel>();
            //services.AddTransient<IStats, Stats>();
            //services.AddTransient<IEntitySet<IID>, EntitySet<IID>>();
        }

        private static void configureAuthentication(IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddAuthentication() //JwtBearerDefaults.AuthenticationScheme)
                //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                //{
                //    var settings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
                //    if (settings == null) return; // swashbuckle
                //    configuration.Bind("JwtSettings", options);
                //    options.TokenValidationParameters.ValidateIssuer = true;
                //    options.TokenValidationParameters.ValidIssuer = settings.Issuer;
                //    options.TokenValidationParameters.ValidateAudience = true;
                //    options.TokenValidationParameters.ValidAudience = settings.Audience;
                //    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                //    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.SecretKey));
                //    //options.SaveToken = true;
                //    //options.RequireHttpsMetadata = true; // already default true
                //})
                //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                //{
                //    var settings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
                //    //configuration.Bind("CookieSettings", options);
                //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                //    options.LoginPath = "mammoth";
                //    options.LogoutPath = "penguin";
                //    options.AccessDeniedPath = "orang";
                //    options.Cookie.Expiration = TimeSpan.FromMinutes(6);
                //    options.ClaimsIssuer = settings.Issuer;
                //    options.SlidingExpiration = true;
                //    options.ReturnUrlParameter = "challenged";
                //})
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["google:clientId"]!;
                    googleOptions.ClientSecret = configuration["google:clientSecret"]!;
                });

            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        var tokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidIssuer = "accounts.google.com",
            //            ValidateAudience = false
            //        };

            //        options.MetadataAddress = "https://accounts.google.com/.well-known/openid-configuration";
            //        options.TokenValidationParameters = tokenValidationParameters;
            //    });

        }

        private static void configureMongo()
        {
            var objectSerializer = new ObjectSerializer(type => 
                ObjectSerializer.DefaultAllowedTypes(type) || type.FullName.StartsWith("souchy.celebi")
            );

            BsonSerializer.RegisterSerializer(objectSerializer);
            BsonSerializer.RegisterSerializer<IID>(new IIDBsonSerializer());
            BsonSerializer.RegisterSerializer<CharacteristicId>(new CharacIdBsonSerializer());


            BsonClassMap.RegisterClassMap<EntitySet<IID>>();
            BsonClassMap.RegisterClassMap<EntityDictionary<CharacteristicId, IStat>>();
            BsonClassMap.RegisterClassMap<Dictionary<CharacteristicId, IStat>>();

            foreach (var t in typeof(IEntity).Assembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsGenericType))
            {
                var m = new BsonClassMap(t);
                m.AutoMap();
                BsonClassMap.RegisterClassMap(m);
            }
        }

    }
}