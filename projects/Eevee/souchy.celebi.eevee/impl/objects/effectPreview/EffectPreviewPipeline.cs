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
    public class EffectPreviewPipeline : IEffectResultPipeline
    {
        public IID sourceID { get; set; }
        public IID targetID { get; set; }
        public IID spellID { get; set; }
        public List<IEffectPreview> triggeredBefore { get; set; }
        public List<IEffectPreview> children { get; set; } = new List<IEffectPreview>();
        public List<IEffectPreview> triggeredAfter { get; set; }

        public void apply(IFight fight)
        {
            foreach (var child in triggeredBefore)
            {
                child.apply(fight);
            }
            foreach (var child in children)
            {
                child.apply(fight);
            }
            foreach (var child in triggeredAfter)
            {
                child.apply(fight);
            }
        }
    }
}
