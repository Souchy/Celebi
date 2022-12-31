using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util.math;

namespace Espeon.souchy.celebi.eeveeimpl.objects
{
    public class Cell : ICell
    {
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }
        public IID modelId { get; set; }

        public IPosition position { get; init; } = new Position();
        public List<IID> statusIds { get; init; } = new List<IID>();
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public Dictionary<ContextType, IContext> contextsStats { get; set; } = new Dictionary<ContextType, IContext>();

        private readonly IUIdGenerator _uIdGenerator;

        public Cell(IUIdGenerator uIdGenerator)
        {
            _uIdGenerator = uIdGenerator;
        }

        public void Dispose()
        {
            _uIdGenerator.dispose(entityUid);
        }
    }
}
