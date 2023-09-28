using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.espeon.eevee.impl.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using souchy.celebi.eevee.enums.characteristics;

namespace EeveeUnitTests.souchy.celebi.eevee.unittest.spells
{
    internal class SpellActionTest
    {
        private IFight fight;
        protected Actions actions;
        protected IActionSpell spellAction;
        protected ICreature caster;
        protected ICreature target;
        protected IStats casterStats;
        protected IStats casterStats2;
        protected IStats targetStats;
        protected IStats targetStats2;

        public SpellActionTest(IFight fight, IActionSpell spellAction, ICreature target)
        {
            this.fight = fight;
            this.actions = new Actions();
            this.spellAction = spellAction;
            this.caster = fight.creatures.Get(spellAction.caster);
            this.target = target;
        }

        protected ISpell getSpell() => fight.spells.Get(spellAction.spell);

        public void act()
        {
            casterStats = caster.GetTotalStats(spellAction);
            targetStats = target.GetTotalStats(spellAction);

            actions.castSpell(spellAction);

            casterStats2 = caster.GetTotalStats(spellAction);
            targetStats2 = target.GetTotalStats(spellAction);
        }
        public void assertEffects(int dmg)
        {
            // Assert Target
            assertStats(targetStats, targetStats2, Resource.Life, -dmg);
            Assert.Equal(dmg, targetStats2.GetStatSimpleValue(Contextual.DamageTaken));
            Assert.Equal(dmg, casterStats2.GetStatSimpleValue(Contextual.DamageDone));

            // Assert Caster
            assertStats(casterStats, casterStats2, Resource.Mana, -getSpell().GetStats().GetStatSimpleValue(Resource.Mana));
        }

        public static void assertStats(IStats initial, IStats final, CharacteristicType statid, int diff)
        {
            Assert.Equal(initial.GetStatSimpleValue(statid) + diff, final.GetStatSimpleValue(statid));
        }

    }
}
