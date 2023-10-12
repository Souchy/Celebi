using Godot;
using MongoDB.Bson;
using souchy.celebi.eevee;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.espeon.eevee.impl.controllers;
using Xunit.Abstractions;
using Resource = souchy.celebi.eevee.enums.characteristics.creature.Resource;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.spells
{
    [Collection(nameof(DiamondFixture))]
    public class TestFireball // : IClassFixture<FightFixture1>
    {
        private readonly ITestOutputHelper output;
        private IFight fight;

        public TestFireball(ITestOutputHelper output)
        {
            this.output = output;
            this.fight = new FightInstance1().fight;
        }

        [Fact]
        public void testUniqueFight()
        {
            var target = fight.timeline.getCreatures().Last();
            var targetStats = target.GetTotalStats(null);
            var lifeIni = targetStats.GetValue<IStatSimple, int>(Resource.Life);
            Assert.Equal(1800, lifeIni);
        }

        private IActionSpell createAction()
        {
            var caster = fight.timeline.slots.First().getCreature();
            var target = fight.timeline.getCreatures().Last();
            var spellAction = new ActionSpell()
            {
                fight = fight,
                player = caster.GetCurrentOwner(),
                caster = caster.entityUid, // Première créature dans la timeline
                targetCell = target.GetCell().entityUid, // Dernière créature dans timeline = différent player
                spell = fight.spells.Values.First(s => s.GetModel().entityUid.Equals(SpellIDs.fireball)).entityUid //fight.spells.Keys.First() // normalement le premier est fireball, id: 646a933fea5ee922a0d0f1eb
            };
            return spellAction;
        }


        [Fact]
        public void testDamage()
        {
            // Arrange
            var actions = new Actions();
            var spellAction = createAction();
            var caster = fight.timeline.slots.First().getCreature();
            var target = fight.timeline.getCreatures().Last();

            var targetStats = target.GetTotalStats(spellAction);
            var life1 = targetStats.GetValue<IStatSimple, int>(Resource.Life);
            var mana1 = targetStats.GetValue<IStatSimple, int>(Resource.Mana);

            // Act
            actions.castSpell(spellAction);

            // Assert
            var targetStats2 = target.GetTotalStats(spellAction);
            var life2 = targetStats2.GetValue<IStatSimple, int>(Resource.Life);
            var mana2 = targetStats2.GetValue<IStatSimple, int>(Resource.Mana);

            Assert.NotEqual(life1, life2);
            output.WriteLine($"Target Life: {life1} -> {life2}");
            //Assert.NotEqual(mana1, mana2);
            //output.WriteLine($"Caster Mana: {mana1} -> {mana2}");



        }

        [Fact]
        public void testStatusDamage()
        {
            // Arrange
            var actions = new Actions();
            var spellAction = createAction();
            var caster = fight.timeline.slots.First().getCreature();
            var target = fight.timeline.getCreatures().Last();

            // Act
            actions.castSpell(spellAction);

            // Check that the status has been created
            Assert.Single(fight.statuses.Keys);

            var targetStats2 = target.GetTotalStats(spellAction);
            var life2 = targetStats2.GetValue<IStatSimple, int>(Resource.Life);

            // Act pass turn to trigger passive burn status damage
            foreach (var c in fight.timeline.getCreatures())
            {
                var pass = new ActionPass()
                {
                    fight = fight,
                    player = caster.GetCurrentOwner(),
                    caster = caster.entityUid
                };
                actions.passTurn(pass);
            }
            // Assert
            var targetStats3 = target.GetTotalStats(spellAction);
            var life3 = targetStats3.GetValue<IStatSimple, int>(Resource.Life);
            Assert.NotEqual(life2, life3);
        }

    }
}
