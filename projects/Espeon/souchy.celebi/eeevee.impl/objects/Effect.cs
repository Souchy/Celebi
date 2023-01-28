using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.effects;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.objects
{
    public class Effect : IEffect
    {
        public IID fightUid { get; init; }
        public IID modelUid { get; set; }
        public IID entityUid { get; init; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; } = new List<ITrigger>();

        public Effect(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
        }

        public void Dispose()
        {
            Scopes.DisposeIID(fightUid, entityUid);
        }
    }
}