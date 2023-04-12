using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.effectResults
{
    public abstract class EffectResult : IEffectResult
    {
        public IID sourceID { get; set; }
        public IID targetID { get; set; }
        public IID spellID { get; set; }
        public IID effectModelID { get; set; }
        public IID effectInstanceID { get; set; }

        public List<IEffectResult> triggeredBefore { get; set; } = new List<IEffectResult>();
        public List<IEffectResult> triggeredAfter { get; set; } = new List<IEffectResult>();

        public void apply(IFight fight)
        {
            foreach (var child in triggeredBefore)
            {
                child.apply0(fight);
            }
            apply(fight);
            foreach (var child in triggeredAfter)
            {
                child.apply0(fight);
            }
        }
        public abstract void apply0(IFight fight);

    }
}
