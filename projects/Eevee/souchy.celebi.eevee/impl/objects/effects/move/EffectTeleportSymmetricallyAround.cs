using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// 
    /// </summary>
    public class EffectTeleportSymmetricallyAround : Effect, IEffectTeleportSymmetricallyAround
    {
        public IValue<IPosition> Center { get; set; } = new Value<IPosition>();


        private EffectTeleportSymmetricallyAround() { }
        private EffectTeleportSymmetricallyAround(IID id) : base(id) { }
        public static IEffectTeleportSymmetricallyAround Create() => new EffectTeleportSymmetricallyAround(Eevee.RegisterIID<IEffect>());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
