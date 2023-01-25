using souchy.celebi.eevee;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Umbreon.eevee.impl.objects
{
    public class Creature : ICreature
    {
        public event OnChanged Changed;
        public IID originalOwnerUid { get; set; }
        public IID currentOwnerUid { get; set; }
        public IStats stats { get; set; }
        public List<IID> spells { get; set; }
        public IPosition position { get; init; }
        public List<IID> statuses { get; init; }
        public Dictionary<ContextType, IContext> contexts { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
            => Changed(propertyType, propertyPath, newValue, oldValue);
    }
}
