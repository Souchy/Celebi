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

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.setup
{
    internal class Services
    {
        public void makeServices()
        {
            var services = new ServiceCollection();
            configureServices(services, null);
        }

        public void configureBuilder(IServiceCollection services, ConfigurationManager configuration)
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
                    Console.WriteLine($"Mongo settings2: {settings}: {{ {settings?.ConnectionString}, {settings?.Federation} }}");
                    if (settings == null) // for swashbuckle 'dotnet swagger' generator. secrets aren't included at that moment
                    {
                        Console.WriteLine("Warning: No MongoSettings to configure services.AddIdentityMongoDbProvider.");
                        return;
                    }
                    mongo.ConnectionString = settings.ConnectionString + "/" + settings.MetaDB;
                    mongo.UsersCollection = nameof(Account);
                }
            );
        }
        public void configureServices(IServiceCollection services, ConfigurationManager configuration)
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
