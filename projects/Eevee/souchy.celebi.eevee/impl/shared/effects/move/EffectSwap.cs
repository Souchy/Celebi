using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    /// <summary>
    /// Swap position with the target if there's a creature there
    /// </summary>
    public class EffectSwap : Effect, IEffectSwap
    {

        private EffectSwap() { }
        private EffectSwap(IID id) : base(id) { }
        public static IEffectSwap Create() => new EffectSwap(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
