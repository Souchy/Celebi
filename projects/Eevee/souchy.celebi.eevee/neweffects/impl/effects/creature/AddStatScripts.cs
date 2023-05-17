using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.neweffects.face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.neweffects.impl.effects.creature
{
    public class AddStatScript : IEffectScript
    {
        public Type SchemaType => typeof(AddStatSchema);

        public IEffectReturnValue apply(IAction action, IEffect e, IEnumerable<IBoardEntity> targets)
        {
            // this is already applied in Creature.GetTotalStats :thinking:
            // but I guess there's instant triggers that dont use statuses ?
            // maybe not, rather use statuses to keep track of ap/mp/.. buffs and reductions 

            //foreach(var t in targets)
            //{
            //    t.
            //}
            throw new NotImplementedException();
        }

        public IEffectPreview preview(IAction action, IEffect e, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
