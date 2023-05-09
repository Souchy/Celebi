using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.status;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.status
{
    public class EffectDispellStatus : Effect, IEffectDispellStatus
    {
        public IStatusCondition Filter { get; set; } //= new StatusCondition();
        //public IValue<bool> DispellCompletely { get; set; }
        public IValue<int> DispellTurns { get; set; } = new Value<int>();


        private EffectDispellStatus() { }
        private EffectDispellStatus(ObjectId id) : base(id) { }
        public static IEffectDispellStatus Create() => new EffectDispellStatus(Eevee.RegisterIIDTemporary());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
