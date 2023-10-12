using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.triggers.schemas
{
    public abstract class TriggerOnSpell : TriggerSchema
    {
        public List<SpellIID> spellIdsInclude { get; set; }
        public List<SpellIID> spellIdsExclude { get; set; }
    }
    public class TriggerOnSpellCast : TriggerOnSpell
    {
        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            return true;
        }

        public override ITriggerSchema copy()
        {
            var copy = new TriggerOnSpellCast();
            copy.spellIdsExclude.AddRange(spellIdsExclude);
            copy.spellIdsInclude.AddRange(spellIdsInclude);
            return copy;
        }
    }
    public class TriggerOnSpellReceive : TriggerOnSpell
    {
        public override bool checkTrigger(IAction action, TriggerEvent triggerEvent)
        {
            return true;
        }

        public override ITriggerSchema copy()
        {
            var copy = new TriggerOnSpellReceive();
            copy.spellIdsExclude.AddRange(spellIdsExclude);
            copy.spellIdsInclude.AddRange(spellIdsInclude);
            return copy;
        }
    }
}
