using souchy.celebi.eevee;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.effects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.controllers
{
    public class Fight : IFight
    {
        #region Properties

        public IID entityUid { get; init; } = Eevee.RegisterIID();

        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public IEntityDictionary<IID, IPlayer> players { get; init; } = new EntityDictionary<IID, IPlayer>();
        public IEntityDictionary<IID, ICreature> creatures { get; init; } = new EntityDictionary<IID, ICreature>();
        public IEntityDictionary<IID, ISpell> spells { get; init; } = new EntityDictionary<IID, ISpell>();
        public IEntityDictionary<IID, IStatus> statuses { get; init; } = new EntityDictionary<IID, IStatus>();
        public IEntityDictionary<IID, ICell> cells { get; init; } = new EntityDictionary<IID, ICell>();
        public IEntityDictionary<IID, IStats> stats { get; init; } = new EntityDictionary<IID, IStats>();
        public IEntityDictionary<IID, IEffect> effects { get; init; } = new EntityDictionary<IID, IEffect>();

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

            Eevee.DisposeIID(this);
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