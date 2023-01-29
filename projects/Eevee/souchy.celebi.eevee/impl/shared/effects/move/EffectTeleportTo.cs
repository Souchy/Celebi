using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public class EffectTeleportTo : Effect, IEffectTeleportTo
    {
        public IValue<IPosition> position { get; set; }
        public EffectTeleportTo() { }
        public EffectTeleportTo(IID id) => entityUid = id;
        public static IEffectTeleportTo Create() => new EffectTeleportTo(Eevee.RegisterIID());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
