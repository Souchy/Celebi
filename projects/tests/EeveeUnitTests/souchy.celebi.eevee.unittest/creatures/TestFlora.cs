using souchy.celebi.eevee;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.espeon.eevee.impl.controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.creatures
{
    [Collection(nameof(DiamondFixture))]
    public class TestFlora : IClassFixture<FightFixture1>
    {
        private readonly ITestOutputHelper output;
        private DiamondFixture diamonds;
        private IFight fight;
        public TestFlora(ITestOutputHelper output, FightFixture1 f, DiamondFixture diamonds)
        {
            this.output = output;
            this.diamonds = diamonds;
            this.fight = f.fight;
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
        public async void testSeedThrow()
        {
        }

        [Fact]
        public void testVineWhip()
        {
            var spells = diamonds.federation.FindSpellsByString("Seed throw");
        }

        [Fact]
        public async void testEntangle()
        {
            //var spells = await diamonds.federation.FindSpellsByString("Entangle");
            //Assert.Single(spells);
            //var entangleModel = spells[0];
            var entangleModel = Eevee.models.spellModels.Get(SpellIDs.entangle);
            var entangle = fight.spells.Values.Single(s => s.modelUid == entangleModel.modelUid);
            var flora = fight.creatures.Values.First(c => c.GetModel().entityUid.Equals(CreatureIDs.flora));
            var targetCell = fight.cells.Values.First();
            var action = new ActionSpell()
            {
                caster = flora.entityUid,
                targetCell = targetCell.entityUid,
                fight = fight,
                spell = entangle.entityUid
            };

            new Actions().castSpell(action);
        }

        [Fact]
        public void testBloom()
        {

        }

    }
}
