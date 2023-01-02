using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.triggers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.zones;

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