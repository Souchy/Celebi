using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.util;

namespace Umbreon.data.resources
{
    public class SpellModel : ISpellModel
    {
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public Dictionary<ResourceType, int> costs { get; set; }
        public ISpellProperties properties { get; set; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public HashSet<IID> effectIds { get; set; }

        public event IEntity.OnChanged Changed;

        public SpellModel()
        {
        }
        public SpellModel(IUIdGenerator uIdGenerator)
        {
            entityUid = uIdGenerator.next();
            nameId = uIdGenerator.next();
            descriptionId = uIdGenerator.next();
        }

        public void Dispose()
        {
        }

        public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
            => Changed(propertyType, propertyPath, newValue, oldValue);
    }
}
