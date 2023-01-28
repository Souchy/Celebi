using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.compiledeffects
{
    public class CompiledDamage : CompiledEffect, ICompiledLifeDamage
    {
        public int damage { get; set; }

        public CompiledDamage() { }
        public CompiledDamage(int damage)
        {
            this.damage = damage;
        }

        public override void apply(IFight fight)
        {
            throw new NotImplementedException();
        }
    }
}
