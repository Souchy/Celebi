using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.interfaces.statuses
{
    public class Trap : ITrap
    {

        public event OnChanged Changed;
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
    }
}