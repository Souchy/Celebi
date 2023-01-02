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

        //private IServiceProvider services => Espeon.scopes[entityUid].ServiceProvider;
        #endregion

        #region Constants
        //public readonly IUIdGenerator uIdGenerator;
        //public readonly IServiceScope scope;
        #endregion

        #region Constructors
        public Fight(ScopeID scopeId) //IUIdGenerator uIdGenerator) //, IBoard board)
        {
            this.entityUid = scopeId;
            this.board = Espeon.GetRequiredScoped<IBoard>(scopeId); //Espeon.scopes[scopeId].ServiceProvider.GetRequiredService<IBoard>();
            //this.uIdGenerator = uIdGenerator;
            //this.board = new Board(this.entityUid);
            //this.entityUid = uIdGenerator.next();
        }
        #endregion


        #region Private Methods
        
        #endregion

        #region Public Methods
        public void Dispose()
        {
            //uIdGenerator.dispose(entityUid);
            //services.GetRequiredService<IUIdGenerator>().dispose(entityUid);
            //Espeon.GetUIdGenerator(entityUid).dispose(entityUid);
            //Espeon.GetRequiredScoped<IUIdGenerator>(entityUid).dispose(entityUid);
            Espeon.DisposeIID(entityUid, entityUid);
            players.ForEach(p => p.Dispose());
            board.Dispose();
        }
        #endregion

    }
}
