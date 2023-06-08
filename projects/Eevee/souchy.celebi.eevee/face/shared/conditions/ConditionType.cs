using Intellenum;

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
    [Intellenum]
    public partial class ConditionType
    {
        // Other / relations
        public static readonly ConditionType LineOfSight            = new(001);
        public static readonly ConditionType DistanceManhattan      = new(002);
        public static readonly ConditionType DistanceX              = new(003);
        public static readonly ConditionType DistanceZ              = new(004);
        public static readonly ConditionType DistancePath           = new(005);

        // Creature
        public static readonly ConditionType CreatureModel          = new(101);
        public static readonly ConditionType CreatureModelSame      = new(102);
        public static readonly ConditionType CreatureCurrentTeam    = new(103);
        public static readonly ConditionType CreatureOriginalTeam   = new(104);
        public static readonly ConditionType CreatureIsSummon       = new(105);
        public static readonly ConditionType CreatureStats          = new(106); // IStats object and use the Condition.comparator
        public static readonly ConditionType CreatureBaseStats      = new(107);
        public static readonly ConditionType CreatureStatsDifference= new(108); // compare caster stats with the target


        // Status
        public static readonly ConditionType StatusModel            = new(201); // statusID / spellID
        public static readonly ConditionType StatusTeam             = new(202);
        public static readonly ConditionType StatusStats            = new(203); // stacks, duration..

        // Spell
        public static readonly ConditionType SpellModel             = new(301);
        public static readonly ConditionType SpellTeam              = new(302);
        public static readonly ConditionType SpellStats             = new(303);
    }

}