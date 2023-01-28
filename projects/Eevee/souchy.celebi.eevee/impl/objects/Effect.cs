﻿using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.objects
{
    public abstract class Effect : IEffect
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID modelUid { get; set; }
        public IID fightUid { get; init; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; }

        public abstract ICompiledEffect compile(IFight fight, IID source, IID targetCell);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
