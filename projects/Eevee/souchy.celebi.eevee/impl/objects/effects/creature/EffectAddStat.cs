using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.enums.characteristics;

namespace souchy.celebi.eevee.impl.objects.effects.creature
{
    public class EffectAddStat : Effect, IEffectAddStat
    {
        public CharacteristicId statId { get; set; }
        public IStat Stat { get; set; }


        private EffectAddStat() { }
        private EffectAddStat(IID id) : base(id) { }
        public static IEffectAddStat Create() => new EffectAddStat(Eevee.RegisterIID<IEffect>());


        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
