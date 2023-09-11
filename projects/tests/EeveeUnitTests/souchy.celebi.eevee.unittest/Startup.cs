using AspNetCore.Identity.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.DependencyInjection;
using System.Diagnostics;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // use Spark's secrets
            var environment = "development";
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .AddUserSecrets<Spark>()
                .Build();

            // Configure Mongo
            Spark.configureMongo(services, configuration);
        }
    }
}
