using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util.math;

namespace Espeon.souchy.celebi.eeveeimpl.objects
{
    public class Cell : ICell
    {
        public uint entityUid { get; init; }
        public uint modelId { get; set; }

        public IPosition position { get; init; } = new Position();
        public List<IStatus> statuses { get; init; } = new List<IStatus>();
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }

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
