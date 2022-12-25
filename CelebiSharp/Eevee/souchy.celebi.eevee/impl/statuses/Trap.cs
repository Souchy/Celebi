using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.statuses;

namespace souchy.celebi.eevee.interfaces.statuses
{
    public class Trap : ITrap
    {
        public uint modelId { get; set; }
        public uint entityUid { get; init; }

        public ICreatureInstance source { get; set; }
        public IBoardEntity holder { get; set; }
        public int delayRemaining { get; set; }
        public int durationRemaining { get; set; }

        public List<ICell> cells { get; set; }
        public List<IEffect> effects { get; set; }
        public IStatDetailed<int> delay { get; set; }
        public IStatDetailed<int> duration { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}