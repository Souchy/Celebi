using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;

namespace souchy.celebi.eevee.interfaces.statuses
{
    public class Trap : ITrap
    {
        public IID fightUid { get; init; }
        public IID modelId { get; set; }
        public IID entityUid { get; init; }

        public IID sourceCreature { get; set; }
        public IID holderEntity { get; set; }
        public int delayRemaining { get; set; }
        public int durationRemaining { get; set; }

        public List<IID> cellIds { get; set; }
        public List<IID> effectIds { get; set; }
        public IStatDetailed<int> delay { get; set; }
        public IStatDetailed<int> duration { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}