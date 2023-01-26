using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : IGlyph
    {
        //public event OnChanged Changed;
        public IID fightUid { get; init; }
        public IID modelUid { get; set; }
        public IID entityUid { get; init; }

        public IID sourceSpell { get; set; }
        public IID sourceCreature { get; set; }
        public IID holderEntity { get; set; }

        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }

        public List<IID> cellIds { get; set; }
        public List<IID> effectIds { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
        //    => Changed(propertyType, propertyPath, newValue, oldValue);
    }
}
