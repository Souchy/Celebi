using souchy.celebi.eevee;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;

namespace Espeon.souchy.celebi.eeveeimpl.controllers
{
    public class Actions
    {
        public void castSpell(IID sourceCreature, IID spellId, IPosition target)
        {
            ISpellModel s = default;
            ICreature source = default;

            // check costs
            foreach (ICost cost in s.costs)
            {
                if (source.stats.get<IStatResource>(cost.resource).current < cost.value)
                {
                    return;
                }
            }
            // check line of sight
            // check target cell filter

            // spend costs
            foreach (ICost cost in s.costs)
            {
                source.stats.get<IStatResource>(cost.resource).current -= cost.value;
            }
            // apply effects
            foreach (IID effectId in s.effectIds)
            {
            }
        }

        public void moveTo(IID sourceCreature, IPosition target)
        {
        }
    }
}