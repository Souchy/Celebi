using souchy.celebi.eevee.face.objects;
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
    /// <summary>
    /// Teleport to target cell or specific cell taken from other values (ex: start pos, first pos around a creature...)
    /// </summary>
    public class EffectTeleportTo : Effect, IEffectTeleportTo
    {
        public IValue<IPosition> Position { get; set; } = new Value<IPosition>();


        private EffectTeleportTo() { }
        private EffectTeleportTo(IID id) => entityUid = id;
        public static IEffectTeleportTo Create() => new EffectTeleportTo(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
