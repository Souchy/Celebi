using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
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
            stats.Set(Resource.Life.Create(90));
            stats.Set(Resource.LifeMax.Create(150));
            //stats.Set(Resource.Life.Create(0));
            //stats.Get<IStatResource>(Resource.Life).current = rng.Next(1, 90);
            //stats.Get<IStatResource>(Resource.Life).currentMax = rng.Next(100, 150);
            //stats.Get<IStatResource>(Resource.Life).initialMax = rng.Next(100, 150);

            stats.Set(Resource.Mana.Create(7));
            stats.Set(Resource.ManaMax.Create(12));
            //stats.Set(Resource.Mana.Create(0));
            //stats.Get<IStatResource>(Resource.Mana).current = rng.Next(1, 10);
            //stats.Get<IStatResource>(Resource.Mana).currentMax = rng.Next(10, 12);
            //stats.Get<IStatResource>(Resource.Mana).initialMax = rng.Next(10, 12);
        }

        private void addSpell(ICreature creature)
        {
            var spell = Scopes.GetRequiredScoped<ISpell>(fight.entityUid);
            //spell.chargesRemaining = rng.Next(1, 5);
            //spell.cooldownRemaining = rng.Next(1, 5);
            spell.GetStats().Set(SpellProperty.RemainingCharges.Create(5));
            spell.GetStats().Set(SpellProperty.RemainingCooldown.Create(3));
            creature.spells.Add(spell.entityUid);
        }
    }
}