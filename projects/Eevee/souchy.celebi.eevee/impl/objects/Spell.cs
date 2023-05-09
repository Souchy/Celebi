using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects
{
    public class Spell : ISpell
    {
        public ObjectId fightUid { get; set; }
        public IID modelUid { get; set; }
        public ObjectId entityUid { get; set; } 

        public ObjectId stats { get; set; }
        //public int chargesRemaining { get; set; }
        //public int cooldownRemaining { get; set; }
        //public int numberOfCastsThisTurn { get => numberOfCastPerEntityThisTurn.Values.Sum(); }
        //public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; }

        private Spell() { }
        private Spell(ObjectId fightId, ObjectId id)
        {
            fightUid = fightId;
            entityUid = id;
        }
        public static ISpell Create(ObjectId fightId) => new Spell(fightId, Eevee.RegisterIIDTemporary());

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            ((ISpell) this).GetStats().Dispose();
        }
    }
}
