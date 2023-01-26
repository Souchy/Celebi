using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.statuses;
using souchy.celebi.eevee.face.util;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace Espeon.souchy.celebi.espeon.eevee.impl.controllers
{
    public class Fight : IFight
    {
        #region Properties

        public event OnChanged Changed;
        public IID entityUid { get; init; }
        //public IBoard board { get; init; }
        //public List<IPlayer> players { get; init; } = new List<IPlayer>();
        //public int currentRound { get; set; }
        //public int currentTurn { get; set; }
        public ITimeline timeline { get; set; }
        public IBoard board { get; set; }
        public Dictionary<IID, IPlayer> players { get; set; }
        public Dictionary<IID, ICreature> creatures { get; init; }
        public Dictionary<IID, ISpell> spells { get; init; }
        public Dictionary<IID, IStatus> statuses { get; init; }
        public Dictionary<IID, ICell> cells { get; init; }

        #endregion Properties

        #region Constructors

        public Fight(ScopeID scopeId)
        {
            this.entityUid = scopeId;
            this.board = Scopes.GetRequiredScoped<IBoard>(scopeId);
        }

        #endregion Constructors



        #region Public Methods

        public void Dispose()
        {
            Scopes.DisposeIID(entityUid, entityUid);
            board.Dispose();
            players.Values.ToList().ForEach(p => p.Dispose());
        }

        public void TriggerChanged(Type propertyType, string propertyPath, object newValue, object oldValue)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}