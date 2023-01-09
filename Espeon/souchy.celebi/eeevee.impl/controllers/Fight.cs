using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.espeon.eevee.impl.controllers
{
    public class Fight : IFight
    {
        #region Properties

        public IID entityUid { get; init; }
        public IBoard board { get; init; }
        public List<IPlayer> players { get; init; } = new List<IPlayer>();
        public int currentRound { get; set; }
        public int currentTurn { get; set; }

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
            players.ForEach(p => p.Dispose());
        }

        #endregion Public Methods
    }
}