using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    /// <summary>
    /// Teleport by x in the direction of the target cell
    /// </summary>
    public class EffectTeleportBy : Effect, IEffectTeleportBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
