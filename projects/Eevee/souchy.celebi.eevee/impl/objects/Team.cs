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
        public string name { get; set; }
    }
}
