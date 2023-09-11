using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{
    public abstract class TriggerOnSpell : ITriggerSchema
    {
        public List<SpellIID> spellIdsInclude { get; set; }
        public List<SpellIID> spellIdsExclude { get; set; }
    }
    public class TriggerOnSpellCast : TriggerOnSpell
    {
    }
    public class TriggerOnSpellReceive : TriggerOnSpell
    {
    }
}
