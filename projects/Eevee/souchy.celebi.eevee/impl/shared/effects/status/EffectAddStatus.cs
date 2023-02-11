using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    /// <summary>
    /// 
    /// </summary>
    public class EffectAddStatus : Effect, IEffectAddStatus
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();


        private EffectAddStatus() { }
        private EffectAddStatus(IID id) : base(id) { }
        public static IEffectAddStatus Create() => new EffectAddStatus(Eevee.RegisterIID<IEffect>());

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
