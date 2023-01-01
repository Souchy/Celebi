using Microsoft.Extensions.Hosting;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Espeon.souchy.celebi.espeon.impl.eevee.controllers
{
    public class Fight : IFight
    {
        #region Properties
        public IID entityUid { get; init; }
        public IBoard board { get; init; }
        public List<IPlayer> players { get; init; } = new List<IPlayer>();
        #endregion

        #region Constants
        public readonly IUIdGenerator uIdGenerator;
        #endregion

        #region Constructors
        public Fight(IUIdGenerator uIdGenerator) //, IBoard board)
        {
            this.uIdGenerator = uIdGenerator;

            this.board = new Board(this.entityUid);

            this.entityUid = uIdGenerator.next();
        }
        #endregion

        #region Public Methods
        public void Dispose()
        {
            uIdGenerator.dispose(entityUid);
            players.ForEach(p => p.Dispose());
            board.Dispose();
        }
        #endregion

    }
}
