using souchy.celebi.eevee.face.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    /// <summary>
    /// 5 creatures per team -> this makes better drafts like LoL:  A, BB, AA, B... A, B, A, B
    /// 3 on the board at a time
    /// </summary>
    public class Team : ITeam
    {
        public ObjectId entityUid { get; set; } = ObjectId.GenerateNewId();
        public string name { get; set; }
        /// <summary>
        /// WIP to think about.
        /// Not sure, maybe instead have alliedPlayers in the Player object.
        /// I think 1 team already refers to multiple players.
        /// </summary>
        //public List<ObjectId> alliedTeams { get; set; } = new();


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
