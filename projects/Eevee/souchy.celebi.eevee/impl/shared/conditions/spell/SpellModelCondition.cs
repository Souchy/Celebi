using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.conditions.spell
{
    public class SpellModelCondition : Condition
    {
        public SpellIID spellModelId { get; set; }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            //if (!checkChildren(action, trigger, boardSource, boardTarget)) // fightId, source, target)) 
            //    return false;

            //IID checkable = this.actorType == ActorType.Source ? source : target;
            ICreature creature = (ICreature) (actorType == ActorType.Source ? boardSource : boardTarget);

            //IFight fight = Eevee.fights.Get(fightId);
            //ICreature creature = action.fight.creatures.Get(checkable);
            ISpell spell = creature.GetSpells().FirstOrDefault(s => s.modelUid == spellModelId);

            return spell != null;
        }

        public override ICondition copyImplementation()
        {
            var copy = new SpellModelCondition();
            copy.spellModelId = spellModelId;
            return copy;
        }

    }
}
