using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util.math;

namespace Espeon.souchy.celebi.eeveeimpl.controllers
{
    public class Actions
    {

        public void castSpell(ICreatureInstance source, IPosition target, ISpell s)
        {
            // check costs
            foreach (ICost cost in s.costs)
            {
                if(source.stats.get<IStatDetailed<int>>(cost.statId).current < cost.val)
                {
                    return;
                }
            }
            // check line of sight
            // check target cell filter

            // spend costs
            foreach (ICost cost in s.costs)
            {
                source.stats.get<IStatDetailed<int>>(cost.statId).current -= cost.val;
            }
            // apply effects
            foreach(IEffect eff in s.effects)
            {

            }

        }

    }
}
