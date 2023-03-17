using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.controllers
{
    public class Fight : IFight
    {
        #region Properties

        public IID entityUid { get; set; } = Eevee.RegisterIID<IFight>();

        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public IEntityDictionary<IID, IPlayer> players { get; init; } = EntityDictionary<IID, IPlayer>.Create();
        public IEntityDictionary<IID, ICreature> creatures { get; init; } = EntityDictionary<IID, ICreature>.Create();
        public IEntityDictionary<IID, ISpell> spells { get; init; } = EntityDictionary<IID, ISpell>.Create();
        public IEntityDictionary<IID, IStatus> statuses { get; init; } = EntityDictionary<IID, IStatus>.Create();
        public IEntityDictionary<IID, ICell> cells { get; init; } = EntityDictionary<IID, ICell>.Create();
        public IEntityDictionary<IID, IStats> stats { get; init; } = EntityDictionary<IID, IStats>.Create();
        public IEntityDictionary<IID, IEffect> effects { get; init; } = EntityDictionary<IID, IEffect>.Create();

        #endregion Properties

        #region Constructors

        public Fight(ScopeID scopeId)
        {
            //this.entityUid = scopeId;
            //this.board = Scopes.GetRequiredScoped<IBoard>(entityUid);
            Eevee.fights.Add(this.entityUid, this);
        }

        #endregion Constructors



        #region Public Methods

        public void Dispose()
        {
            //Scopes.DisposeIID(entityUid, entityUid);

            Eevee.DisposeIID<IFight>(this.entityUid);
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