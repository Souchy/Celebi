using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;
using static souchy.celebi.eevee.face.objects.IEffect;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.objects
{
    public abstract class Effect : IEffect
    {
        public IID entityUid { get; set; } //= Eevee.RegisterIID();
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }


        #region Dynamic status creation
        public StatusProperties statusProperties { get; set; } = null;
        #endregion

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public IZone zone { get; set; } = new Zone();
        public IEntityList<ITrigger> triggers { get; set; } = new EntityList<ITrigger>(); // EntityList<ITrigger>.Create();

        /// <summary>
        /// children
        /// </summary>
        public IEntityList<IID> effectIds { get; set; } = new EntityList<IID>(); 


        protected Effect() { }
        protected Effect(IID id) => entityUid = id;

        public abstract IEffectResult compile(IFight fight, IAction action, TriggerEvent trigger);

        public IEnumerable<IEffect> GetEffects() => effectIds.Values.Select(i => this.GetFight()?.effects.Get(i) ?? Eevee.models.effects.Get(i));

        public IEnumerable<EffectResult> checkTriggers(TriggerOrderType orderType, EffectResult e)
        {
            throw new NotImplementedException();
        }

        public void CopyBasicTo(IEffect e)
        {
            e.sourceCondition = sourceCondition;
            e.targetFilter = targetFilter;
            e.zone = zone;
            e.triggers = triggers;
            e.effectIds = effectIds;
            e.statusProperties = statusProperties;
        }

        public void Dispose()
        {
            Eevee.DisposeIID<IEffect>(entityUid);
        }

    }
}
