using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.controllers
{
    public class Board : IBoard
    {
        #region Properties

        public event OnChanged Changed;
        public IID fightUid { get; init; }
        public IID entityUid { get; init; }

        public List<IID> creatureIds { get; init; } = new List<IID>();
        public List<IID> cells { get; init; } = new List<IID>();

        //private readonly IFight instances;
        //private IEnumerable<ICreature> Creatures => creatureIds.Select(cid => instances.creatures[cid]);

        #endregion Properties

        #region Constructors

        public Board(ScopeID scopeId)
        {
            this.fightUid = scopeId;
            this.entityUid = Scopes.GetUIdGenerator(fightUid).next();
            //this.instances = Scopes.GetRequiredScoped<IFight>(fightUid);
        }

        public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
            => Changed?.Invoke(propertyType, propertyPath, newValue, oldValue);

        public void Dispose()
        {
            // nothing to dispose? 
            throw new NotImplementedException();
        }

        #endregion Constructors

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