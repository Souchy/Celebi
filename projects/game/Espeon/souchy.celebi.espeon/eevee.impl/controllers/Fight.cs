using MongoDB.Bson;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.espeon.eevee.impl.controllers
{
    public class Fight : IFight
    {
        #region Properties

        public ObjectId entityUid { get; set; } = Eevee.RegisterIIDTemporary();

        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public IEntityDictionary<ObjectId, IPlayer> players { get; init; } = EntityDictionary<ObjectId, IPlayer>.Create();
        public IEntityDictionary<ObjectId, ICreature> creatures { get; init; } = EntityDictionary<ObjectId, ICreature>.Create();
        public IEntityDictionary<ObjectId, ISpell> spells { get; init; } = EntityDictionary<ObjectId, ISpell>.Create();
        public IEntityDictionary<ObjectId, IStatusContainer> statuses { get; init; } = EntityDictionary<ObjectId, IStatusContainer>.Create();
        public IEntityDictionary<ObjectId, ICell> cells { get; init; } = EntityDictionary<ObjectId, ICell>.Create();

        public IEntityDictionary<ObjectId, IStats> stats { get; init; } = EntityDictionary<ObjectId, IStats>.Create();
        public IEntityDictionary<ObjectId, IEffect> effects { get; init; } = EntityDictionary<ObjectId, IEffect>.Create();

        #endregion Properties

        #region Constructors

        public Fight(ScopeID scopeId)
        {
            //this.entityUid = scopeId;
            //this.board = Scopes.GetRequiredScoped<IBoard>(entityUid);
            Eevee.fights.Add(entityUid, this);
        }

        #endregion Constructors



        #region Public Methods

        public void Dispose()
        {
            //Scopes.DisposeIID(entityUid, entityUid);
            Eevee.DisposeEventBus(this);
            board.Dispose();
            players.Dispose(); //players.Values.ToList().ForEach(p => p.Dispose());
            creatures.Dispose();
            spells.Dispose();
            statuses.Dispose();
            cells.Dispose();
            stats.Dispose();
            effects.Dispose();
        }

        #endregion Public Methods
    }
}
