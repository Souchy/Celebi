using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;
using MongoDB.Bson;

namespace souchy.celebi.eevee.impl.objects.effects.creature
{
    public class EffectAddStat : Effect, IEffectAddStat
    {
        public CharacteristicId statId { get; set; }
        public IStat stat { get; set; }

        private EffectAddStat() { }
        private EffectAddStat(ObjectId id) : base(id) { }
        public static IEffectAddStat Create() => new EffectAddStat(Eevee.RegisterIIDTemporary());


        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
