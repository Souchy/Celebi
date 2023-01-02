using Espeon.souchy.celebi.eeevee.impl;
using Espeon.souchy.celebi.eeevee.impl.controllers;
using Espeon.souchy.celebi.espeon.eevee.impl.controllers;
using Espeon.souchy.celebi.espeon.eevee.impl.objects;
using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util.math;

namespace Espeon.souchy.celebi.espeon.impl
{
    public class Example
    {
        //private readonly IDiamondModels diamond = new DiamondModels();

        private readonly IFight fight;
        private readonly Random rng = new Random();

        public Example(IFight fight)
        {
            this.fight = fight;

            addPlayer();
            addPlayer();

            Console.WriteLine("Test 1");
        }

        private void addPlayer()
        {
            var player = Espeon.GetRequiredScoped<IPlayer>(fight.entityUid);
            addCreature(player);
            addCreature(player);
        }
        private void addCreature(IPlayer player)
        {
            // create
            var creature = Espeon.GetRequiredScoped<ICreature>(fight.entityUid);
            creature.modelUid = Espeon.GetUIdGenerator(fight.entityUid).next();
            // player control
            player.creatures.Add(creature.entityUid);
            creature.playerUid = player.entityUid; 
            // add to board
            Espeon.GetRequiredScoped<IBoard>(fight.entityUid).creatureIds.Add(creature.entityUid);
            creature.position.set(rng.Next(10), rng.Next(10));
            // ...
            addStats(creature);
            addSpell(creature);
            addSpell(creature);
        }
        private void addStats(ICreature creature)
        {
            creature.stats.set(StatType.Life, new StatResource());
            creature.stats.get<IStatResource>(StatType.Life).current = rng.Next(1, 90);
            creature.stats.get<IStatResource>(StatType.Life).currentMax = rng.Next(100, 150);
            creature.stats.get<IStatResource>(StatType.Life).initialMax = rng.Next(100, 150);

            creature.stats.set(StatType.Mana, new StatResource());
            creature.stats.get<IStatResource>(StatType.Life).current = rng.Next(1, 10);
            creature.stats.get<IStatResource>(StatType.Life).currentMax = rng.Next(10, 12);
            creature.stats.get<IStatResource>(StatType.Life).initialMax = rng.Next(10, 12);
        }
        private void addSpell(ICreature creature)
        {
            var spell = Espeon.GetRequiredScoped<ISpell>(fight.entityUid);
            spell.chargesRemaining = rng.Next(1, 5);
            spell.cooldownRemaining = rng.Next(1, 5);
            creature.spells.Add(spell.entityUid);
        }

    }
}
