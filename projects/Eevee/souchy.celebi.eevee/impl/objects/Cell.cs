using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.impl.objects
{
    public class Cell : ICell
    {
        public event OnChanged Changed;
        public IID entityUid { get; init; }
        public IID modelUid { get; set; }
        public IID fightUid { get; init; }
        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public IPosition position { get; init; }
        public List<IID> statuses { get; init; }
        public Dictionary<ContextType, IContext> contexts { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
