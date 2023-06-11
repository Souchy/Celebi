using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions.spell;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.conditions;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.impl.shared.conditions.value
{
    public class SpellCondition : Condition, ISpellCondition
    {
        public int spellModelId { get; set; }
        public IStats conditionStats { get; set; }
        //public CharacteristicId statId { get; set; }
        //public object value { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if (!this.checkChildren(action, trigger, boardSource, boardTarget)) // fightId, source, target)) 
                return false;
                
            //IID checkable = this.actorType == ActorType.Source ? source : target;
            ICreature creature = (ICreature) (this.actorType == ActorType.Source ? boardSource : boardTarget);

            //IFight fight = Eevee.fights.Get(fightId);
            //ICreature creature = action.fight.creatures.Get(checkable);
            ISpell spell = creature.GetSpells().FirstOrDefault(s => s.modelUid == spellModelId);

            var spellStats = spell.GetStats(); //TODO .GetTotalStats(action); //.@base;

            foreach (var conditionPair in conditionStats.@base.Pairs)
            {
                if (!spellStats.Has(conditionPair.Key)) return false;
                var stat = spellStats.Get(conditionPair.Key);
                if (!this.comparator.check(stat.genericValue, conditionPair.Value.genericValue))
                    return false;
            }
            return true;

            //IStat stat = spell.GetStats().Get(statId);
            //// var stat = creature.GetStats().Get((StatType) statId);
            
            //object fetchedValue = null;
            //if(stat is StatSimple statSimple) {
            //    fetchedValue = statSimple.value;
            //}
            //if(stat is StatBool statBool) {
            //    fetchedValue = statBool.value;
            //}
            //return this.comparator.check(fetchedValue, value);
        }
    }
}