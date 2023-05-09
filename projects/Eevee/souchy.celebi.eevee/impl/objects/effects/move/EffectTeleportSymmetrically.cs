using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;
using MongoDB.Bson;

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// Teleport the caster
    /// </summary>
    public class EffectTeleportSymmetrically : Effect, IEffectTeleportSymmetrically
    {
        private EffectTeleportSymmetrically() { }
        private EffectTeleportSymmetrically(ObjectId id) : base(id) { }
        public static IEffectTeleportSymmetrically Create() => new EffectTeleportSymmetrically(Eevee.RegisterIIDTemporary());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
