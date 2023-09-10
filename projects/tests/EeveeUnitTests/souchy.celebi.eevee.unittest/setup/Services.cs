using AspNetCore.Identity.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using souchy.celebi.spark;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.setup
{
    internal class Services
    {
        public void makeServices()
        {

            //var configuration = new ConfigurationBuilder()
            //    .AddUserSecrets<Settings>()
            //    .Build();
            //ConfigurationManager manager;
            //ConfigurationManager.AppSettings[""];
            //var configuration = new ConfigurationBuilder()
            //    .AddUserSecrets<HiromiContext>() // This should be the name of the class that inherits from DbContext.
            //    //.AddUserSecrets(Assembly.GetEntryAssembly()) You can also do this if your database context and database design factory classes are in the same project, which should be the case 99% of the time
            //    .Build();
            var environment = "development";
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .AddUserSecrets<Spark>()
                .Build();

            var settings = configuration.GetSection(nameof(MongoSettings)).Get<MongoSettings>();
            Console.WriteLine($"Services: config: {configuration}");

            var services = new ServiceCollection();
            configureBuilder(services, configuration);
            configureServices(services, configuration);
        }

        public void configureBuilder(IServiceCollection services, IConfiguration configuration)
        {
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
                    Console.WriteLine($"[UTest] Mongo settings2: {settings}: {{ {settings?.ConnectionString}, {settings?.Federation} }}");
                    if (settings == null) // for swashbuckle 'dotnet swagger' generator. secrets aren't included at that moment
                    {
                        Console.WriteLine("[UTest] Warning: No MongoSettings to configure services.AddIdentityMongoDbProvider.");
                        return;
                    }
                    mongo.ConnectionString = settings.ConnectionString + "/" + settings.MetaDB;
                    mongo.UsersCollection = nameof(Account);
                }
            );
        }
        public void configureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Mongo    
            services.Configure<MongoSettings>(configuration.GetSection(nameof(MongoSettings))); // relates to appsettings.json
            services.AddSingleton<MongoClientService>();
            services.AddSingleton<MongoFederationService>();
            services.AddSingleton<MongoModelsDbService>();
            services.AddSingleton<MongoFightsDbService>();
            services.AddSingleton<MongoMetaDbService>();
            services.AddSingleton<MongoI18NDbService>();
        }


    }
}
