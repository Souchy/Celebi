using Intellenum;
using souchy.celebi.eevee.impl.shared.conditions;
using souchy.celebi.eevee.impl.shared.conditions.creature;
using souchy.celebi.eevee.impl.shared.conditions.other;
using souchy.celebi.eevee.impl.shared.conditions.spell;
using souchy.celebi.eevee.impl.shared.conditions.status;

namespace souchy.celebi.eevee.face.shared.conditions
{
    //public enum ConditionType
    //{
    //    Team, // teamEnumId = val
    //    CreatureModel, // modelid = val
    //    IsSummon,

    //    Stat, // statId = val1 && statValue = val2 (can be int or bool)
    //    Status, // statusId = val1 && statId = val2 && statValue = val3
    //    Spell,  // spellId = val1 && statId = val2 && statValue = val3

    //    Distance, // dist = val
    //    Height, // height = val
    //    LineOfSight, // haveLos = val


    //    Moment, // momentEnumId = val
    //}


    // (SchemaFactory, Script)
    [Intellenum<ConditionEnumType>]
    public partial class ConditionType
    {
        // Other / relations
        public static readonly ConditionType LineOfSight            = new(new ConditionEnumType(001, typeof(LineOfSightCondition)));
        public static readonly ConditionType Distance               = new(new ConditionEnumType(002, typeof(DistanceCondition)));
        //public static readonly ConditionType DistanceX              = new(new ConditionEnumType(003, typeof(StatsCondition)));
        //public static readonly ConditionType DistanceZ              = new(new ConditionEnumType(004, typeof(StatsCondition)));
        //public static readonly ConditionType DistancePath           = new(new ConditionEnumType(005, typeof(StatsCondition)));

        // Creature
        // creature.stats.other -> isSummon, //currentTeam, originalTeam -> creatures dont rly have a team, just an owner, it's up to the condition to determine wether that owner is ally or enemy
        public static readonly ConditionType CreatureModel          = new(new ConditionEnumType(101, typeof(CreatureModelCondition)));
        public static readonly ConditionType CreatureModelSame      = new(new ConditionEnumType(102, typeof(CreatureModelCondition)));
        public static readonly ConditionType CreatureCurrentTeam    = new(new ConditionEnumType(103, typeof(CreatureCurrentTeamCondition)));
        public static readonly ConditionType CreatureOriginalTeam   = new(new ConditionEnumType(104, typeof(CreatureOriginalTeamCondition)));
        public static readonly ConditionType CreatureIsSummon       = new(new ConditionEnumType(105, typeof(CreatureOriginalTeamCondition)));
        public static readonly ConditionType CreatureStats          = new(new ConditionEnumType(106, typeof(CreatureStatsCondition))); // IStats object and use the Condition.comparator
        //public static readonly ConditionType CreatureNaturalStats   = new(new ConditionEnumType(107, typeof(StatsCondition)));
        //public static readonly ConditionType CreatureStatsDifference= new(new ConditionEnumType(108, typeof(StatsCondition))); // compare caster stats with the target

        // Status
        public static readonly ConditionType StatusModel            = new(new ConditionEnumType(201, typeof(StatusModelCondition))); // statusID / spellID
        public static readonly ConditionType StatusTeam             = new(new ConditionEnumType(202, typeof(StatusTeamCondition)));
        public static readonly ConditionType StatusStats            = new(new ConditionEnumType(203, typeof(StatusStatsCondition))); // stacks, duration..

        // Spell
        public static readonly ConditionType SpellModel             = new(new ConditionEnumType(301, typeof(SpellModelCondition)));
        //public static readonly ConditionType SpellTeam              = new(new ConditionEnumType(302, typeof(Condition))); // what?how?why?
        public static readonly ConditionType SpellStats             = new(new ConditionEnumType(303, typeof(SpellStatsCondition)));
    }

    public record class ConditionEnumType(int id, Type conditionType) : IComparable<ConditionEnumType>
    {
        public int CompareTo(ConditionEnumType other) => id - other.id;
    }

}