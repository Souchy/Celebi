﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.impl.shared;
using Spark.Models;

namespace Spark.Services
{
    public class CreatureModelService
    {
        private readonly IMongoCollection<CreatureModel> _creatureModelsCollection;

        //public class CreatureModelDatabaseSettings
        //{
        //    public string ConnectionString { get; set; } = null!;

        //    public string DatabaseName { get; set; } = null!;

        //    public string CreatureModelsCollectionName { get; set; } = null!;
        //}

        public CreatureModelService(IOptions<CelebiModelsDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _creatureModelsCollection = mongoDatabase.GetCollection<CreatureModel>(nameof(CreatureModel));
                //celebiModelsDatabaseSettings.Value.CreatureModelsCollectionName);
        }

        public async Task<List<CreatureModel>> GetAsync() =>
            await _creatureModelsCollection.Find(_ => true).ToListAsync();

        public async Task<CreatureModel?> GetAsync(string id) =>
            await _creatureModelsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CreatureModel newCreatureModel) =>
            await _creatureModelsCollection.InsertOneAsync(newCreatureModel);

        public async Task UpdateAsync(string id, CreatureModel updatedCreatureModel) =>
            await _creatureModelsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedCreatureModel);

        public async Task RemoveAsync(string id) =>
            await _creatureModelsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
