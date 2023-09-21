using souchy.celebi.eevee;
using souchy.celebi.eevee.enums.characteristics;
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

        [Fact]
        public void testDamage()
        {
            // Arrange
            var caster = fight.timeline.slots.First().getCreature();
            var target = fight.timeline.getCreatures().Last();
            var spellAction = new ActionSpell()
            {
                fight = fight,
                caster = caster.entityUid, // Première créature dans la timeline
                targetCell = target.GetCell().entityUid, // Dernière créature dans timeline = différent player
                spell = fight.spells.Values.First().entityUid // normalement le premier est fireball, id: 646a933fea5ee922a0d0f1eb
            };

            var test = new SpellActionTest(fight, spellAction, target);

            // Act
            test.act();

            // Assert
            test.assertEffects(dmg: 15);
        }


    }

    /// <summary>
    /// TODO
    /// </summary>
    //public class AssertExtensions : Assert
    //{
    //    public static void EqualStats(IStats initial, IStats final, CharacteristicType statid, int diff)
    //    {
    //        Assert.Equal(initial.GetStatSimpleValue(statid) + diff, final.GetStatSimpleValue(statid));
    //    }
    //}

}
