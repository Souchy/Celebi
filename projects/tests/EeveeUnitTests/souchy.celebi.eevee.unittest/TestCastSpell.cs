using souchy.celebi.eevee;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.espeon.eevee.impl.controllers;
using Xunit.Abstractions;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest
{
    [Collection(nameof(DiamondFixture))]
    public class TestCastSpell : IClassFixture<FightFixture1>
    {
        private readonly ITestOutputHelper output;
        private IFight fight;
        public TestCastSpell(ITestOutputHelper output, FightFixture1 f)
        {
            this.output = output;
            this.fight = f.fight;
        }

        private IActionSpell createAction()
        {
            var caster = fight.timeline.creatureIds.First();
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
            var caster = fight.timeline.creatureIds.First();
            var target = fight.timeline.getCreatures().Last();

            var targetStats = target.GetTotalStats(spellAction);
            var lifeIni = targetStats.Get<IStatSimple>(Resource.Life).value;
            var mana1 = targetStats.Get<IStatSimple>(Resource.Mana).value;

            // Act
            actions.castSpell(spellAction);

            // Assert
            var lifeFinal = target.GetTotalStats(spellAction).Get<IStatSimple>(Resource.Life).value;
            var mana2 = targetStats.Get<IStatSimple>(Resource.Mana).value;

            Assert.NotEqual(lifeIni, lifeFinal);
            output.WriteLine($"Target Life: {lifeIni} -> {lifeFinal}");
            //Assert.NotEqual(mana1, mana2);
            //output.WriteLine($"Caster Mana: {mana1} -> {mana2}");
        }

    }
}
