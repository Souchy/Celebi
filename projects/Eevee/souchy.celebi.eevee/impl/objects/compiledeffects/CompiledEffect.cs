using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.compiledeffects
{
    public abstract class CompiledEffect : ICompiledEffect
    {
        public IID sourceID { get; set; }
        public IID targetID { get; set; }
        public IID spellID { get; set; }
        public IID effectModelID { get; set; }
        public IID effectInstanceID { get; set; }

        public abstract void apply(IFight fight);

    }
}
