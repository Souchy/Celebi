using souchy.celebi.eevee.neweffects.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{

    public abstract class TriggerOnEffect : TriggerSchema
    {
        public List<EffT> effectTypesInclude { get; set; }
        public List<EffT> effectTypesExclude { get; set; }
    }
    public class TriggerOnEffectCast : TriggerOnEffect
    {
    }
    public class TriggerOnEffectReceive : TriggerOnEffect
    {
    }

}
