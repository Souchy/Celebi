﻿using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public class EffectTranslateTo : Effect, IEffectTranslateTo
    {
        public IValue<IPosition> Position { get; set; } = new Value<IPosition>();


        private EffectTranslateTo() { }
        private EffectTranslateTo(IID id) : base(id) { }
        public static IEffectTranslateTo Create() => new EffectTranslateTo(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
