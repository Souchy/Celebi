using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.shared.conditions;
using souchy.celebi.eevee.impl.shared.conditions.creature;
using souchy.celebi.eevee.impl.shared.conditions.other;
using souchy.celebi.eevee.impl.shared.conditions.spell;
using souchy.celebi.eevee.impl.shared.conditions.status;
using souchy.celebi.eevee.impl.util;
using System.Diagnostics;

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


    public sealed record ConditionType
    {
        public IID id { get; init; }
        public string name { get; init; }
        [JsonIgnore]
        [BsonIgnore]
        public Type schemaType { get; init; }
        public ConditionType(int id, Type schemaType) 
        {
            this.id = new IID(id.ToString());
            this.name = schemaType.Name;
            this.schemaType = schemaType;
        }
        public Condition createInstance()
        {
            return (Condition) Activator.CreateInstance(schemaType);
        }
        
        public static readonly ConditionType Group                  = new ConditionType(001, typeof(GroupCondition));

        // Other / relations
        public static readonly ConditionType LineOfSight            = new ConditionType(002, typeof(LineOfSightCondition));
        public static readonly ConditionType Distance               = new ConditionType(003, typeof(DistanceCondition));
        //public static readonly ConditionType DistanceX              = new ConditionType(003, typeof(StatsCondition));
        //public static readonly ConditionType DistanceZ              = new ConditionType(004, typeof(StatsCondition));
        //public static readonly ConditionType DistancePath           = new ConditionType(005, typeof(StatsCondition));

        // Creature
        // creature.stats.other -> isSummon, //currentTeam, originalTeam -> creatures dont rly have a team, just an owner, it's up to the condition to determine wether that owner is ally or enemy
        public static readonly ConditionType CreatureModel          = new ConditionType(101, typeof(CreatureModelCondition));
        public static readonly ConditionType CreatureModelSame      = new ConditionType(102, typeof(CreatureModelCondition));
        public static readonly ConditionType CreatureCurrentTeam    = new ConditionType(103, typeof(CreatureCurrentTeamCondition));
        public static readonly ConditionType CreatureOriginalTeam   = new ConditionType(104, typeof(CreatureOriginalTeamCondition));
        public static readonly ConditionType CreatureIsSummon       = new ConditionType(105, typeof(CreatureOriginalTeamCondition));
        public static readonly ConditionType CreatureStats          = new ConditionType(106, typeof(CreatureStatsCondition)); // IStats object and use the Condition.comparator
        //public static readonly ConditionType CreatureNaturalStats   = new ConditionType(107, typeof(StatsCondition));
        //public static readonly ConditionType CreatureStatsDifference= new ConditionType(108, typeof(StatsCondition)); // compare caster stats with the target

        // Status
        public static readonly ConditionType StatusModel            = new ConditionType(201, typeof(StatusModelCondition)); // statusID / spellID
        public static readonly ConditionType StatusTeam             = new ConditionType(202, typeof(StatusTeamCondition));
        public static readonly ConditionType StatusStats            = new ConditionType(203, typeof(StatusStatsCondition)); // stacks, duration..

        // Spell
        public static readonly ConditionType SpellModel             = new ConditionType(301, typeof(SpellModelCondition));
        //public static readonly ConditionType SpellTeam              = new ConditionType(302, typeof(Condition)); // what?how?why?
        public static readonly ConditionType SpellStats             = new ConditionType(303, typeof(SpellStatsCondition));


        private static List<ConditionType> _values = new();
        static ConditionType() => _values.AddRange(StaticEnumUtils.findValues<ConditionType>());
        public static ConditionType[] values() => _values.ToArray();
        public static ConditionType get(IID id) => _values.Find(v => v.id == id);
        public static ConditionType getByType(Type schemaType) => _values.Find(v => v.schemaType == schemaType);
        public static ConditionType getByName(string schemaName) => _values.Find(v => v.schemaType.Name.Equals(schemaName));
    }

}