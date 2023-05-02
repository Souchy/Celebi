using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.spell
{
    public class EffectAddSpellRange : Effect, IEffectAddSpellRange
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();
        public IValue<int> Value { get; set; } = new Value<int>();

        private EffectAddSpellRange() { }
        private EffectAddSpellRange(IID id) : base(id) { }
        public static IEffectAddSpellRange Create() => new EffectAddSpellRange(Eevee.RegisterIID<IEffect>());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
