using Microsoft.Extensions.DependencyInjection;
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
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public Fight(ScopeID scopeId) 
        {
            this.entityUid = scopeId;
            this.board = Espeon.GetRequiredScoped<IBoard>(scopeId);
        }
        #endregion


        #region Private Methods
        
        #endregion

        #region Public Methods
        public void Dispose()
        {
            Espeon.DisposeIID(entityUid, entityUid);
            board.Dispose();
            players.ForEach(p => p.Dispose());
        }
        #endregion

    }
}
