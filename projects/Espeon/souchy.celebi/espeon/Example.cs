using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.stats;

namespace Espeon.souchy.celebi.espeon
{
    public class Example
    {
        //private readonly IDiamondModels diamond = new DiamondModels();

        private readonly IFight fight;
        private readonly Random rng = new Random();

        // create fight
        // create board
        // create players?
        // create 6 creatures
        // create all their spells
        // create map instance
        public Example(IFight fight)
        {
            this.fight = fight;
            Console.WriteLine("fight id: " + fight.entityUid);

            addPlayer();
            addPlayer();

            Console.WriteLine("Test 1");
        }

        private void addPlayer()
        {
            var player = Scopes.GetRequiredScoped<IPlayer>(fight.entityUid);
            addCreature(player);
            addCreature(player);
        }

        private void addCreature(IPlayer player)
        {
            // create
            var creature = Scopes.GetRequiredScoped<ICreature>(fight.entityUid);
            creature.modelUid = Scopes.GetUIdGenerator(fight.entityUid).next();
            // player control
            player.creatures.Add(creature.entityUid);
            creature.currentOwnerUid = player.entityUid;
            // add to board
            Scopes.GetRequiredScoped<IBoard>(fight.entityUid).creatureIds.Add(creature.entityUid);
            creature.position.set(rng.Next(10), rng.Next(10));
            // add stats, spells...
            addStats(creature);
            addSpell(creature);
            addSpell(creature);
        }

        private void addStats(ICreature creature)
        {
            var stats = fight.stats.Get(creature.stats);
            stats.set(StatType.Life, new StatResource());
            stats.get<IStatResource>(StatType.Life).current = rng.Next(1, 90);
            stats.get<IStatResource>(StatType.Life).currentMax = rng.Next(100, 150);
            stats.get<IStatResource>(StatType.Life).initialMax = rng.Next(100, 150);

            stats.set(StatType.Mana, new StatResource());
            stats.get<IStatResource>(StatType.Mana).current = rng.Next(1, 10);
            stats.get<IStatResource>(StatType.Mana).currentMax = rng.Next(10, 12);
            stats.get<IStatResource>(StatType.Mana).initialMax = rng.Next(10, 12);
        }

        private void addSpell(ICreature creature)
        {
            var spell = Scopes.GetRequiredScoped<ISpell>(fight.entityUid);
            spell.chargesRemaining = rng.Next(1, 5);
            spell.cooldownRemaining = rng.Next(1, 5);
            creature.spells.Add(spell.entityUid);
        }
    }
}