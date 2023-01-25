using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using System.Collections.Generic;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Umbreon.data.resources
{
    public class CreatureModel : ICreatureModel
    {
        public event OnChanged Changed;
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public HashSet<IID> skins { get; init; } = new HashSet<IID>();

        public IStats baseStats { get; init; } = new Stats();
        public HashSet<IID> baseSpells { get; init; } = new HashSet<IID>();
        public HashSet<IID> baseStatusPassives { get; init; } = new HashSet<IID>();

        public CreatureModel()
        {
        }

        public CreatureModel(IUIdGenerator uIdGenerator)
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
