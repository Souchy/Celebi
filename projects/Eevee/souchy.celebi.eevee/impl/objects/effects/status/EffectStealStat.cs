using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.status;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.enums.characteristics;

namespace souchy.celebi.eevee.impl.objects.effects.status
{
    public class EffectStealStat : Effect, IEffectStealStat
    {
        public CharacteristicId statId { get; set; }
        public IValue<int> Value { get; set; }


        private EffectStealStat() { }
        private EffectStealStat(IID id) : base(id) { }
        public static IEffectStealStat Create() => new EffectStealStat(Eevee.RegisterIID<IEffect>());

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
