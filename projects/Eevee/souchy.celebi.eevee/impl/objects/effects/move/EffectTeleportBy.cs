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

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// Teleport by x in the direction of the target cell
    /// </summary>
    public class EffectTeleportBy : Effect, IEffectTeleportBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        public override IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger)
        {
            throw new NotImplementedException();
        }
    }
}
