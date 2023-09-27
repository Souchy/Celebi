using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.impl.shared.conditions.creature
{
    public class CreatureOriginalTeamCondition : Condition
    {
        public Dictionary<TeamRelationType, bool> team { get; set; }

        public CreatureOriginalTeamCondition()
        {
            foreach (var teamRelationType in Enum.GetValues<TeamRelationType>())
                this.team.Add(teamRelationType, true);
        }

        public override bool check(IAction action, TriggerEvent trigger, ICreature boardSource, IBoardEntity boardTarget)
        {
            throw new NotImplementedException();
        }
    }
}
