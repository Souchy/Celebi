﻿using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects.special
{
    public interface IEffectFly : IEffect
    {
        public IValue<int> height { get; set; }

    }
}
