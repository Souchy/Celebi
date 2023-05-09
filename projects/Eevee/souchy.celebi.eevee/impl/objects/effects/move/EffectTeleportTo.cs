using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// Teleport to target cell or specific cell taken from other values (ex: start pos, first pos around a creature...)
    /// </summary>
    public class EffectTeleportTo : Effect, IEffectTeleportTo
    {
        public IValue<IPosition> Position { get; set; } = new Value<IPosition>();


        private EffectTeleportTo() { }
        private EffectTeleportTo(ObjectId id) => entityUid = id;
        public static IEffectTeleportTo Create() => new EffectTeleportTo(Eevee.RegisterIIDTemporary());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
