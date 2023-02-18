using static souchy.celebi.eevee.enums.StatValueType;
using static souchy.celebi.eevee.enums.StatCategory;
using static souchy.celebi.eevee.enums.ElementType;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using System.Reflection;

namespace souchy.celebi.eevee.enums
{

    #region Util
    [AttributeUsage(AttributeTargets.Field)]
    public class StatTypePropertiesAttribute : Attribute //, INamedExtendedEnumAttribute
    {
        public readonly StatValueType valueType;
        public readonly StatCategory category;
        public readonly ElementType element;
        public StatTypePropertiesAttribute(StatValueType valueType, StatCategory category, ElementType element = Water)
        {
            this.valueType = valueType;
            this.category = category;
            this.element = element;
        }
    }
    public enum StatValueType
    {
        Simple,
        Detailed,
        Bool,
        Resource
    }
    public enum StatCategory
    {
        Resource,
        Affinity,
        Resistance,
        State,
        Other
    }
    public enum ElementType
    {
        Water,
        Fire,
        Earth,
        Air,
        /// <summary>
        /// True damage. Not sure if it's an element or just a EffectTrueDamage
        /// </summary>
        True
    }
    #endregion

    public enum StatType
    {
        #region Resources & Limits & Regens
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Life,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Mana,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Movement,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Rage,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Summons,

        [StatTypeProperties(Simple, StatCategory.Resource)]
        LifeRegen, // -1 = full every turn, 0 = no regen, 1 = 1/turn, 0.25 = 1 per 4 turns // string to parse for different regens
        [StatTypeProperties(Simple, StatCategory.Resource)]
        ManaRegen,
        [StatTypeProperties(Simple, StatCategory.Resource)]
        MovementRegen,
        [StatTypeProperties(Simple, StatCategory.Resource)]
        RageRegen,
        #endregion


        #region Affinities & Resistances
        // elemental
        [StatTypeProperties(Simple, Affinity, Fire)]
        FireAffinity,
        [StatTypeProperties(Simple, Affinity, Water)]
        WaterAffinity,
        [StatTypeProperties(Simple, Affinity, Air)]
        AirAffinity,
        [StatTypeProperties(Simple, Affinity, Earth)]
        EarthAffinity,

        [StatTypeProperties(Simple, Resistance, Fire)]
        FireResistance,
        [StatTypeProperties(Simple, Resistance, Water)]
        WaterResistance,
        [StatTypeProperties(Simple, Resistance, Air)]
        AirResistance,
        [StatTypeProperties(Simple, Resistance, Earth)]
        EarthResistance,

        // others
        [StatTypeProperties(Simple, Affinity)]
        GlobalDamageAffinity,
        [StatTypeProperties(Simple, Affinity)]
        MeleeAffinity,
        [StatTypeProperties(Simple, Affinity)]
        DistanceAffinity,
        [StatTypeProperties(Simple, Affinity)]
        HealAffinity,
        [StatTypeProperties(Simple, Affinity)]
        TrapAffinity,
        [StatTypeProperties(Simple, Affinity)]
        GlyphAffinity,
        [StatTypeProperties(Simple, Affinity)]
        PoisonAffinity,

        [StatTypeProperties(Simple, Resistance)]
        GlobalDamageResistance,
        [StatTypeProperties(Simple, Resistance)]
        MeleeResistance,
        [StatTypeProperties(Simple, Resistance)]
        DistanceResistance,
        [StatTypeProperties(Simple, Resistance)]
        HealResistance,
        [StatTypeProperties(Simple, Resistance)]
        TrapResistance,
        [StatTypeProperties(Simple, Resistance)]
        GlyphResistance,
        [StatTypeProperties(Simple, Resistance)]
        PoisonResistance,
        #endregion

        #region Other
        [StatTypeProperties(Simple, Other)]
        Range,
        [StatTypeProperties(Simple, Other)]
        Speed, // Initiative in timeline
        [StatTypeProperties(Simple, Other)]
        Echo, // like Poe shrine: repeat spells x times
        [StatTypeProperties(Simple, Other)]
        Erosion, // life erosion % on damage
        #endregion

        #region States ?
        [StatTypeProperties(Bool, State)]
        Visible,
        [StatTypeProperties(Bool, State)]
        Ghosted, // phasing // is 30% opacity, unlocks line of sight, but blocks movement

        [StatTypeProperties(Bool, State)]
        Flying, // slash Hovering
        [StatTypeProperties(Bool, State)]
        Underground,

        // order of counters:
        // wet > hot > grounded > shocked
        [StatTypeProperties(Bool, State)]
        Drenched, // wet
        [StatTypeProperties(Bool, State)]
        Hot, // burning
        [StatTypeProperties(Bool, State)]
        Shocked,
        [StatTypeProperties(Bool, State)]
        Grounded, // Muddy

        [StatTypeProperties(Bool, State)]
        Rooted, // can't translate (dash/push/pull)
        [StatTypeProperties(Bool, State)]
        Gravity, // can't teleport
        [StatTypeProperties(Bool, State)]
        Heavy, // carry/throw
        [StatTypeProperties(Bool, State)]
        Unmoveable, // everything 

        [StatTypeProperties(Bool, State)]
        Carrying,
        [StatTypeProperties(Bool, State)]
        Carried,

        [StatTypeProperties(Bool, State)]
        Pacifist
        #endregion
    }

    #region Sub types
    public enum ResourceType
    {
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Life = StatType.Life,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Mana = StatType.Mana,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Movement = StatType.Movement,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Rage = StatType.Rage,
        [StatTypeProperties(StatValueType.Resource, StatCategory.Resource)]
        Summons = StatType.Summons,

        // TODO ResourceMax
        //LifeMax = StatType.LifeMax,
        //ManaMax = StatType.ManaMax,
        //MovementMax = StatType.MovementMax,
        //RageMax = StatType.RageMax,
        //SummonsMax = StatType.SummonsMax,

        //// -1 = full every turn, 0 = no regen, 1 = 1/turn, 0.25 = 1 per 4 turns // string to parse for different regens
        //LifeRegen = StatType.LifeRegen, 
        //ManaRegen = StatType.ManaRegen,
        //MovementRegen = StatType.MovementRegen,
        //RageRegen = StatType.RageRegen
    }

    public enum StateType
    {
        Visible = StatType.Visible,
        Ghosted, // phasing // is 30% opacity, unlocks line of sight, but blocks movement

        Flying, // slash Hovering
        Underground,

        Drenched, // wet
        Shocked, // 
        Hot, // burning
        Grounded, // Muddy

        Rooted, // can't translate (dash/push/pull)
        Gravity, // can't teleport
        Heavy, // carry/throw
        Unmoveable, // everything 

        Carrying,
        Carried,

        Pacifist
    }
    #endregion

    public static class StatTypeExtensions
    {
        public static StatTypePropertiesAttribute GetProperties(this StatType statType)
        {
            var attr = statType.GetType()
                    .GetField(Enum.GetName(statType))
                    .GetCustomAttribute(typeof(StatTypePropertiesAttribute), true);
            return (StatTypePropertiesAttribute) attr;
        }
        public static StatTypePropertiesAttribute GetProperties(this ResourceType statType) => 
            statType.GetStatType().GetProperties();
        public static StatType GetAffinity(this ElementType stat) =>
            (StatType) Enum.Parse(typeof(StatType), Enum.GetName(stat) + "Affinity");
        public static StatType GetResistance(this ElementType stat) =>
            (StatType) Enum.Parse(typeof(StatType), Enum.GetName(stat) + "Resistance");
        public static StatType GetStatType(this ResourceType stat) => 
            (StatType) Enum.Parse(typeof(StatType), Enum.GetName(stat));
        public static StatType GetStatType(this StateType stat) => 
            (StatType) Enum.Parse(typeof(StatType), Enum.GetName(stat));
        public static IStat Create(this StatType type) =>
            type.GetProperties().valueType.Create(type);
        public static IStat Create(this StatValueType valueType, StatType st) =>
            valueType switch
            {
                StatValueType.Simple => StatSimple.Create(st), //new StatSimple(st),
                StatValueType.Detailed => StatDetailed.Create(st), //new StatDetailed(st),
                StatValueType.Bool => StatBool.Create(st), //new StatBool(st),
                StatValueType.Resource => StatResource.Create(st), //new StatResource(st),
                _ => throw new Exception()
            };
    }

}
