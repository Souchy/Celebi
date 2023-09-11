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
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.impl.util;
using MongoDB.Bson;
using souchy.celebi.eevee.impl.shared;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.statuses;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest
{
    [Collection(nameof(DiamondFixture))]
    public class FightFixture1 : IDisposable
    {
        public IFight fight { get; set; }
        public FightFixture1(DiamondFixture fix)
        {
            fight = Fight.Create();
            fight.settings = new FightSettings(); // Eevee.models.defaultFightSettings; // TODO load fight settigns from DB. Could have multiple modes with their own names ("normal", "draft", "blitz", "urf"...)

            IMap tempMap = Map.Create(); //  Eevee.models.maps.Values.First();
            tempMap.cells = new ICell[6];

            IBoard board = Board.Create(fight.entityUid);
            // TODO copy cells to board?
            // what do I even need a board for if all the creatures/cells are in Fight/Timeline and the positions are in the IBoardEntities
            //  just move the board functions to the fight directly maybe

            ITimeline timeline = Timeline.Create(fight.entityUid);

            var creatureModels = Eevee.models.creatureModels.Values.ToArray();
            if (creatureModels.Length == 0) 
                return;
            //Assert.NotEmpty(creatureModels);

            var nc = fight.settings.numberOfcreaturesOnBoardPerTeam;

            // Creatures + Cells for 2 teams
            for (int j = 0; j < fight.settings.numberOfTeams; j++)
            {
                var player = Player.Create(fight.entityUid);
                player.team = new Team();
                player.team.name = "Team " + j;
                // for 3 creatures
                for (int i = 0; i < nc; i++)
                {
                    // Cell
                    ICell cell = Cell.Create();
                    cell.position.set(i, j);
                    cell.type = CellType.floor;
                    tempMap.cells[nc * j + i] = cell;

                    // Creature
                    ICreatureModel creaModel = creatureModels[nc * j + i];
                    ICreature crea = configureCreature(creaModel);
                    crea.position.set(i, j);
                    crea.originalOwnerUid = player.entityUid;
                    crea.currentOwnerUid = player.entityUid;
                    player.creatures.Add(crea.entityUid); // add to player
                    //timeline.creatureIds.Add(crea.entityUid); // add to timeline/board
                }
            }

            // Timeline ordering
            int team = 0;
            IPlayer? player1 = null;
            var ordered = fight.creatures.Values.OrderBy(c => c.GetNaturalStats().Get<IStatSimple>(OtherProperty.Speed)?.value).ToList();
            while(ordered.Any())
            {
                if(player1 == null) player1 = ordered.First().GetCurrentOwner();
                ICreature? nextCrea = null;
                if(team % 2 == 0)
                {
                    nextCrea = ordered.First(c => c.GetOriginalOwner() == player1);
                } else
                {
                    nextCrea = ordered.First(c => c.GetOriginalOwner() != player1);
                }
                ordered.Remove(nextCrea);
                fight.timeline.creatureIds.Add(nextCrea.entityUid);
                team++;
            }

            // Add map cells to the fight
            foreach (var cell in tempMap.cells)
            {
                fight.cells.Add(cell.entityUid, cell);
            }
        }

        private ICreature configureCreature(ICreatureModel creaModel)
        {
            // Creature
            ICreature crea = Creature.Create(fight.entityUid);
            crea.modelUid = creaModel.modelUid;

            // Stats
            CreatureStats modelStats = creaModel.GetBaseStats();
            //if(modelStats.Get<IStatSimple>(Resource.LifeInitialMax))
            CreatureStats creaStats = CreatureStats.Create();
            modelStats.copyToFight(fight.entityUid, creaStats);
            creaStats.setStarterCurrentValues();
            crea.statsId = creaStats.entityUid;

            // Spells
            foreach (ISpellModel spellModel in creaModel.GetSpells())
            {
                if (spellModel == null)
                {
                    Debug.WriteLine("Missing spellModel in creature " + creaModel.entityUid);
                    continue;
                }
                ISpell spell = Spell.Create(fight.entityUid);
                spell.modelUid = spellModel.modelUid;
                SpellStats spellStats = SpellStats.Create();
                spellModel.GetStats().copyToFight(fight.entityUid, spellStats);
                spell.statsId = spellStats.entityUid;
                crea.spellIds.Add(spell.entityUid);
            }
            // Passives
            foreach (IStatusModel statusModel in creaModel.GetStatusPassives())
            {
                if (statusModel == null)
                {
                    Debug.WriteLine("Missing statusModel in creature " + creaModel.entityUid);
                    continue;
                }
                IStatusContainer status = StatusContainer.Create(fight.entityUid);
                status.modelUid = statusModel.modelUid;
                status.sourceCreature = crea.entityUid;
                status.holderEntity = crea.entityUid;
                status.statsId = statusModel.GetStats().copyToFight(fight.entityUid, StatusContainerStats.Create()).entityUid;
                crea.statuses.Add(status.entityUid);
            }
            return crea;
        }

        public void Dispose()
        {
        }
    }
}
