using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.schemas;
using souchy.celebi.spark.models.aggregations;
using souchy.celebi.spark.services;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest
{
    public class DiamondFixture : IAsyncLifetime
    {
        public readonly MongoFederationService federation;
        public readonly CollectionService<IFight> _fights;
        public readonly CollectionService<ICreatureModel> _creatures;
        public readonly CollectionService<ISpellModel> _spells;
        public readonly CollectionService<SpellModelAggregation> _spellsView;
        public readonly CollectionService<IStats> _stats;
        public readonly CollectionService<IEffect> _effects;
        public readonly CollectionService<IStatusModel> _statuses;
        public readonly CollectionService<IMap> _maps;
        public DiamondFixture(MongoModelsDbService db, MongoFederationService federation)
        {
            this.federation = federation;
            _fights = db.GetMongoService<IFight>();
            _creatures = db.GetMongoService<ICreatureModel>();
            _spells = db.GetMongoService<ISpellModel>();
            _spellsView = db.GetMongoService<SpellModelAggregation>();
            _effects = db.GetMongoService<IEffect>();
            _stats = db.GetMongoService<IStats>();
            _statuses = db.GetMongoService<IStatusModel>();
            _maps = db.GetMongoService<IMap>();
        }

        public async Task InitializeAsync()
        {
            var creatures = await _creatures.GetDictionaryAsync();
            Eevee.models.creatureModels.AddAll(creatures);
            var spells = await _spells.GetDictionaryAsync();
            Eevee.models.spellModels.AddAll(spells);
            var effects = await _effects.GetDictionaryAsync();
            Eevee.models.effects.AddAll(effects);
            var statuses = await _statuses.GetDictionaryAsync();
            Eevee.models.statusModels.AddAll(statuses);
            var stats = await _stats.GetDictionaryAsync();
            Eevee.models.stats.AddAll(stats);
            var maps = await _maps.GetDictionaryAsync();
            Eevee.models.maps.AddAll(maps);
        }
        public Task DisposeAsync() => Task.CompletedTask;
    }

    [CollectionDefinition(nameof(DiamondFixture))]
    public class DiamondCollection : ICollectionFixture<DiamondFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
