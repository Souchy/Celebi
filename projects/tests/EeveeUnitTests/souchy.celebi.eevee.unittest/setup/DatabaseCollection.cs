using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models.aggregations;
using souchy.celebi.spark.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.util.math;
using souchy.celebi.espeon.eevee.impl.controllers;
using Player = souchy.celebi.eevee.impl.objects.Player;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.setup
{
    /// <summary>
    /// Maybe I could even setup a few pre-set Fights instances, saved in database and reload them in Eevee.fights with everything already setup. <br></br>
    /// Just like loading a Fight state / replay
    /// </summary>
    /// <param name="db"></param>
    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    public class DatabaseFixture : IDisposable
    {
        public readonly CollectionService<IFight> _fights;
        public readonly CollectionService<ICreatureModel> _creatures;
        public readonly CollectionService<ISpellModel> _spells;
        public readonly CollectionService<SpellModelAggregation> _spellsView;
        public readonly CollectionService<IStats> _stats;
        public readonly CollectionService<IEffect> _effects;
        public readonly CollectionService<IStatusModel> _statuses;
        public readonly CollectionService<IMap> _maps;
        public DatabaseFixture(MongoModelsDbService db)
        {
            Console.WriteLine("DbFixture ctor");
            _fights = db.GetMongoService<IFight>();
            _creatures = db.GetMongoService<ICreatureModel>();
            _spells = db.GetMongoService<ISpellModel>();
            _spellsView = db.GetMongoService<SpellModelAggregation>();
            _effects = db.GetMongoService<IEffect>();
            _stats = db.GetMongoService<IStats>();
            _statuses = db.GetMongoService<IStatusModel>();
            _maps = db.GetMongoService<IMap>();
            setup();
        }
        private async void setup()
        {
            Console.WriteLine($"DbFixture setup collections");
            loadModels(); // need to wait until everything is loaded before creating the fights
            Console.WriteLine($"DbFixture setup models: creatureModel count = {Eevee.models.creatureModels.Values.Count()}");
            setupFight1();
            setupFight2();
        }
        private async void loadModels()
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
        public void setupFight1()
        {
            var fight = new Fight();
            fight.settings = new FightSettings(); // Eevee.models.defaultFightSettings; // TODO load fight settigns from DB. Could have multiple modes with their own names ("normal", "draft", "blitz", "urf"...)
            IMap map = Eevee.models.maps.Values.First();
            // TODO cells? 
            IBoard board = Board.Create(fight.entityUid);
            // TODO copy cells to board?
            // what do I even need a board for if all the creatures/cells are in Fight/Timeline and the positions are in the IBoardEntities
            //  just move the board functions to the fight directly maybe
            ITimeline timeline = Timeline.Create(fight.entityUid);

            // for 2 teams
            for(int j = 0; j < fight.settings.numberOfTeams; j++)
            {
                var player = Player.Create(fight.entityUid);
                player.team = new Team();
                player.team.name = "Team " + j;
                // for 3 creatures
                for (int i = 0; i < fight.settings.numberOfcreaturesOnBoardPerTeam; i++)
                {
                    ICreatureModel creaModel = Eevee.models.creatureModels.Values.ToArray()[i * j + i];
                    // Creature
                    var crea = Creature.Create(fight.entityUid);
                    player.creatures.Add(crea.entityUid); // add to player
                    timeline.creatureIds.Add(crea.entityUid); // add to timeline/board
                    crea.position.set(i, j);
                    crea.modelUid = creaModel.modelUid;
                    crea.statsId = creaModel.GetBaseStats().copyToFight(fight.entityUid).entityUid;
                    // Spells
                    foreach(ISpellModel spellModel in creaModel.GetSpells())
                    {
                        var spell = Spell.Create(fight.entityUid);
                        spell.modelUid = spellModel.modelUid;
                        spell.statsId = spellModel.GetStats().copyToFight(fight.entityUid).entityUid;
                        crea.spellIds.Add(spell.entityUid); 
                    }
                    // Passives
                    foreach(IStatusModel statusModel in creaModel.GetStatusPassives())
                    {
                        var status = StatusContainer.Create(fight.entityUid);
                        status.modelUid = statusModel.modelUid;
                        status.sourceCreature = crea.entityUid;
                        status.holderEntity = crea.entityUid;
                        status.statsId = statusModel.GetStats().copyToFight(fight.entityUid).entityUid;
                        crea.statuses.Add(status.entityUid);
                    }
                }
            }
        }
        public void setupFight2()
        {

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
