using souchy.celebi.eevee;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.espeon.eevee.impl.controllers;
using Xunit.Abstractions;

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
            var caster = fight.timeline.slots.First().creatureId;
            var target = fight.timeline.getCreatures().Last();
            var spellAction = new ActionSpell()
            {
                fight = fight,
                caster = caster, // Première créature dans la timeline
                targetCell = target.GetCell().entityUid, // Dernière créature dans timeline = différent player
                spell = fight.spells.Keys.First() // normalement le premier est fireball, id: 646a933fea5ee922a0d0f1eb
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
            var lifeIni = targetStats.GetValue<IStatSimple, int>(Resource.Life);
            var mana1 = targetStats.GetValue<IStatSimple, int>(Resource.Mana);

            // Act
            actions.castSpell(spellAction);

            // Assert
            var targetStats2 = target.GetTotalStats(spellAction);
            var lifeFinal = targetStats2.GetValue<IStatSimple, int>(Resource.Life);
            var mana2 = targetStats2.GetValue<IStatSimple, int>(Resource.Mana);

            Assert.NotEqual(lifeIni, lifeFinal);
            output.WriteLine($"Target Life: {lifeIni} -> {lifeFinal}");
            //Assert.NotEqual(mana1, mana2);
            //output.WriteLine($"Caster Mana: {mana1} -> {mana2}");
        }

    }
}
