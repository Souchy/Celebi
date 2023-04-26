using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.objects.effectReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.effects.meta
{
    internal interface IValue2
    {
        public Type valueReturnType { get; set; }
    }
    public class ValueGetter : IValue2
    {
        public Type valueReturnType { get; set; }
    }

    public class EffectValueNumber : Effect, IValue<int>
    {
        public int value { get; set; }
        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            value = 2;
            return null;
        }
        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) => null;
    }

}
