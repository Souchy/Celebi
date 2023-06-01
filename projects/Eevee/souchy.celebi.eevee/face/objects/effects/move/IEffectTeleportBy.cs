﻿using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.effects.move
{
    public interface IEffectTeleportBy : IEffect
    {
        public IValue<IPosition> Delta { get; set; }
    }
}