using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.conditions;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class SpellCondition : Condition, ISpellCondition
    {
        public int spellModelId { get; set; }
        public StatType statId { get; set; }
        public object value { get; set; }

        public override bool check(IID fightId, IID source, IID target)
        {
            if(!this.checkChildren(fightId, source, target)) 
                return false;
                
            IID checkable = this.actorType == ActorType.Source ? source : target;
            var fight = Eevee.fights.Get(fightId);
            var creature = fight.creatures.Get(checkable);
            var spell = creature.GetSpells().FirstOrDefault(s => s.modelUid == spellModelId);
            var stat = spell.GetStats().Get(statId);
            // var stat = creature.GetStats().Get((StatType) statId);
            
            object fetchedValue = null;
            if(stat is StatSimple statSimple) {
                fetchedValue = statSimple.value;
            }
            if(stat is StatBool statBool) {
                fetchedValue = statBool.value;
            }
            return this.comparator.check(fetchedValue, value);
        }
    }
}