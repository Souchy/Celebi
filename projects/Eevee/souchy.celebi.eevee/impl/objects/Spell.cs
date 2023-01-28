using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    public class Spell : ISpell
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID modelUid { get; set; }
        public IID fightUid { get; init; }
        public int chargesRemaining { get; set; }
        public int cooldownRemaining { get; set; }
        public int numberOfCastsThisTurn { get; set; }
        public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
