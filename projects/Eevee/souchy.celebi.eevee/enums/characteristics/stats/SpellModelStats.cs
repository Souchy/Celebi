﻿using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.other
{
    public class SpellModelStats : Stats
    {
        public static new IStats Create() => new SpellModelStats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };
        //----- Model
        // los
        // max charges
        // max cast per target
        // max cast per turn
        // cd
        // cd initial
        // cd global
        //// min range
        //// max range
        //// cast in diag
        //// cast in line
    }
}
