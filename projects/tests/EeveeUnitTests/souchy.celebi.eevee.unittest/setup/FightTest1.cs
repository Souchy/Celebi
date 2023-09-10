using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee;
using souchy.celebi.espeon.eevee.impl.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player = souchy.celebi.eevee.impl.objects.Player;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.setup
{
    [Collection("DatabaseCollection")]
    public class FightTest1 : IDisposable
    {
        private DatabaseFixture db;
        public FightTest1(DatabaseFixture db)
        {
            Console.WriteLine($"FightTest1 ctor: {db._creatures}");
            this.db = db;
        }

        public void Dispose()
        {
            Console.WriteLine("FighTest1 dispose");
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
            for (int j = 0; j < fight.settings.numberOfTeams; j++)
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
                    foreach (ISpellModel spellModel in creaModel.GetSpells())
                    {
                        var spell = Spell.Create(fight.entityUid);
                        spell.modelUid = spellModel.modelUid;
                        spell.statsId = spellModel.GetStats().copyToFight(fight.entityUid).entityUid;
                        crea.spellIds.Add(spell.entityUid);
                    }
                    // Passives
                    foreach (IStatusModel statusModel in creaModel.GetStatusPassives())
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
    }
}
