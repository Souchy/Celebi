using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;

namespace souchy.celebi.eevee.statuses
{
    public class Status : IStatus
    {
        public IID fightUid { get; init; }
        public IID modelId { get; set; }
        public IID entityUid { get; init; }

        public IID sourceSpell { get; set; }
        public IID sourceCreature { get; set; }
        public IID holderEntity { get; set; }

        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }

        public List<IID> effectIds { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}