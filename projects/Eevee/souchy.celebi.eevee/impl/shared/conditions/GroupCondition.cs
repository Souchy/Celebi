using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared.conditions
{
    /// <summary>
    /// Just check children
    /// </summary>
    public class GroupCondition : Condition
    {
        public ConditionGroupType groupType { get; set; } = ConditionGroupType.AND;
        public IEntityList<ICondition> children { get; set; } = new EntityList<ICondition>();
        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            return checkChildren(action, trigger, boardSource, boardTarget);
        }
        public bool checkChildren(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            if (children.Values.Count == 0)
                return true;
            if (groupType == ConditionGroupType.AND)
            {
                bool result = true;
                foreach (var c in children.Values)
                {
                    result &= c.check(action, trigger, boardSource, boardTarget);
                }
                return result;
            }
            if (groupType == ConditionGroupType.OR)
            {
                bool result = false;
                foreach (var c in children.Values)
                {
                    result |= c.check(action, trigger, boardSource, boardTarget);
                }
                return result;
            }
            return true;
        }

        public override ICondition copyImplementation()
        {
            var copy = new GroupCondition();
            copy.groupType = groupType;
            foreach(var child in children.Values)
                copy.children.Add(child.copy());
            return copy;
        }
    }
}
