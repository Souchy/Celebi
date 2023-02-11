using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.effects.move;
using souchy.celebi.eevee.face.shared.effects.status;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.impl.shared.effects.status
{
    public class EffectStealStat : Effect, IEffectStealStat
    {
        public StatType type { get; set; }
        public IValue<int> Value { get; set; }


        private EffectStealStat() { }
        private EffectStealStat(IID id) : base(id) { }
        public static IEffectStealStat Create() => new EffectStealStat(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
