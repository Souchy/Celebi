using souchy.celebi.eevee.enums;
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
    public class EffectPreviewMove : EffectPreview, IEffectResultMove
    {
        public MoveType MoveType { get; set; }
        public IID newCell { get; set; }

        public override void apply0(IFight fight)
        {
            throw new NotImplementedException();
        }
    }
}
