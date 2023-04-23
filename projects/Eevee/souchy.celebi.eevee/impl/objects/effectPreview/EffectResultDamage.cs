using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.effectResults
{
    public class EffectResultDamage : EffectPreview, IEffectResultLifeDamage
    {
        public int damage { get; set; }

        public EffectResultDamage() { }
        public EffectResultDamage(int damage)
        {
            this.damage = damage;
        }

        public override void apply0(IFight fight)
        {
            throw new NotImplementedException();
        }
    }
}
