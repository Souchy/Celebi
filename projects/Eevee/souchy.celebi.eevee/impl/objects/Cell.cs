using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using MongoDB.Bson;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.impl.objects
{
    public class Cell : ICell
    {
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }
        public ObjectId fightUid { get; set; }

        public bool walkable { get; set; }
        public bool blocksLos { get; set; }
        public IPosition position { get; init; } = new Position();
        public IEntitySet<ObjectId> statuses { get; init; } = new EntitySet<ObjectId>();
        public Dictionary<ContextType, IContext> contexts { get; set; } = new();

        private Cell() { }
        private Cell(ObjectId id) => this.entityUid = id;
        public static ICell Create() => new Cell(Eevee.RegisterIIDTemporary());

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            throw new NotImplementedException();
        }

    }
}
