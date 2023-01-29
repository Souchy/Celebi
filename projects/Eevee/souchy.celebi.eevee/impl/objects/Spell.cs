﻿using souchy.celebi.eevee.face.objects;
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
        public IID entityUid { get; set; } //= Eevee.RegisterIID();
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }
        public int chargesRemaining { get; set; }
        public int cooldownRemaining { get; set; }
        public int numberOfCastsThisTurn { get; set; }
        public Dictionary<IID, int> numberOfCastPerEntityThisTurn { get; set; }

        public Spell() { }
        // TODO should take fight id surely
        private Spell(IID id)  => entityUid = id;
        public static ISpell Create() => new Spell(Eevee.RegisterIID());

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}