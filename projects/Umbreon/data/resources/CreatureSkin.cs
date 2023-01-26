using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using System.Collections.Generic;

namespace Umbreon.data.resources
{
    public class CreatureSkin : ICreatureSkin
    {
        public IID entityUid { get; init; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public IID meshModel { get; set; }
        public IID icon { get; set; }
        public IID animations { get; set; }
        public Dictionary<IID, IID> spellSkins { get; set; }
        public Dictionary<IID, IID> effectSkins { get; set; } // FIXME This should go inside ISpellSkin

        public event IEntity.OnChanged Changed;

        public CreatureSkin()
        {
        }
        public CreatureSkin(IUIdGenerator uIdGenerator)
        {
            this.entityUid = uIdGenerator.next();
            this.nameId = uIdGenerator.next();
            this.descriptionId = uIdGenerator.next();
        }


        public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
            => Changed(propertyType, propertyPath, newValue, oldValue);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
