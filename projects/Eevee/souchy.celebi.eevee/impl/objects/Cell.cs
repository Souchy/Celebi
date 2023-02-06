using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.effects.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.impl.objects
{
    public class Cell : ICell
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }

        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public IPosition position { get; init; } = new Position();
        public IEntitySet<IID> statuses { get; init; } = new EntitySet<IID>();
        public Dictionary<ContextType, IContext> contexts { get; set; } = new();

        private Cell() { }
        private Cell(IID id) => this.entityUid = id;
        public static ICell Create() => new Cell(Eevee.RegisterIID<ICell>());

        public void Dispose()
        {
            Eevee.DisposeIID<ICell>(entityUid);
            throw new NotImplementedException();
        }

    }
}
