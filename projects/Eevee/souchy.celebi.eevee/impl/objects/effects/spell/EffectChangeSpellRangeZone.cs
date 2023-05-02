using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.spell;
using souchy.celebi.eevee.face.shared.zones;
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
    /// <summary>
    /// This changes the zones for min/max ranges
    /// Would have a different effect for EffectChangeSpellRangeLength (int min, int max) that just adds to the length of range
    /// </summary>
    public class EffectChangeSpellRangeZone : Effect, IEffectChangeSpellRangeZone
    {
        public IValue<IID> SpellModelId { get; set; } = new Value<IID>();
        public IValue<IZone> RangeMin { get; set; } = new Value<IZone>();
        public IValue<IZone> RangeMax { get; set; } = new Value<IZone>();

        private EffectChangeSpellRangeZone() { }
        private EffectChangeSpellRangeZone(IID id) : base(id) { }
        public static IEffectChangeSpellRangeZone Create() => new EffectChangeSpellRangeZone(Eevee.RegisterIID<IEffect>());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
