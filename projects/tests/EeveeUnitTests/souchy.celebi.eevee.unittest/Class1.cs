using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest
{

    public class DatabaseFixture : IAsyncLifetime
    {
        public string world = "!!!";
        //private MongoModelsDbService db;
        private readonly CollectionService<ICreatureModel> _creatureModels;
        private readonly CollectionService<IStats> _stats;
        public List<ICreatureModel> creatureModels { get; set; } = new List<ICreatureModel>();
        public DatabaseFixture(MongoModelsDbService db)
        {
            _creatureModels = db.GetMongoService<ICreatureModel>();
            _stats = db.GetMongoService<IStats>();
        }

        public async Task InitializeAsync()
        {
            this.creatureModels = await _creatureModels.GetAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }


    [Collection("Database collection")]
    public class ThingFixture : IDisposable
    {
        public string hello = "hello world";
        public readonly DatabaseFixture db;
        public ThingFixture(DatabaseFixture db)
        {
            this.db = db;
            this.db.world = "???";
        }
        public void Dispose()
        {
        }
    }



    [Collection("Database collection")]
    public class Class1 : IClassFixture<ThingFixture>
    {
        private readonly ITestOutputHelper output;
        private ThingFixture fix;
        public Class1(ITestOutputHelper output, ThingFixture f, DatabaseFixture db)
        {
            this.output = output;
            this.fix = f;
            this.output.WriteLine(f.hello + f.db.world + db.world);
        }
        [Fact]
        public void test1()
        {
            this.output.WriteLine($"dic count: {fix.db.creatureModels.Count()} : {string.Join(", ", fix.db.creatureModels.Select(k => k.entityUid.ToString()))}");
            Assert.True(true);
        }


    }
}
