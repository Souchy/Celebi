using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.espeon.eevee.impl.controllers
{
    public class Board : IBoard
    {
        [BsonId]
        public ObjectId entityUid { get; set; } = Eevee.RegisterIIDTemporary();
        public ObjectId fightUid { get; set; }

        public IEntityList<ObjectId> creatureIds { get; init; } = new EntityList<ObjectId>();
        public IEntityList<ObjectId> cells { get; init; } = new EntityList<ObjectId>();

        public Board(ScopeID scopeId)
        {
            //fightUid = scopeId;
            this.GetFight().board = this;
            //this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            //this.instances = Scopes.GetRequiredScoped<IFight>(fightUid);
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            this.GetFight().board = null;
            // nothing to dispose? 
            //throw new NotImplementedException();
        }

        /*
        #region Public Methods

        public ICell getCell(IPosition pos)
        {
            return cells.First(c => c.position == pos);
        }

        public ICreature getCreature(IPosition pos)
        {
            return Creatures.First(c => c.position == pos);
        }

        public List<ICreature> getCreatures(IPosition pos)
        {
            var list = new List<ICreature>();
            foreach (var c in Creatures)
                if (c.position.x == pos.x && c.position.y == pos.y)
                    list.Add(c);
            return list;
        }

        public bool hasCreature(IPosition pos)
        {
            return Creatures.Any(c => c.position == pos);
        }

        public void Dispose()
        {
            Scopes.DisposeIID(fightUid, entityUid);
            this.cells.ForEach(c => c.Dispose());
            this.cells.Clear();
            this.creatureIds.Clear();
        }

        #endregion Public Methods
        */
    }
}
