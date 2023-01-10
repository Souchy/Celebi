namespace souchy.celebi.eevee.enums
{
    public enum StatValueType
    {
        Simple,
        Detailed,
        Bool,
        Resource
    }

    public enum ResourceType
    {
        Life,
        Mana,
        Movement,
        Rage,
        Summons,

        // TODO ResourceMax
        LifeMax,
        ManaMax,
        MovementMax,
        RageMax,
        SummonsMax,
    }

    public enum ElementType
    {
        Fire,
        Water,
        Air,
        Earth,
    }

    public enum StatType
    {
        Life,
        Mana,
        Movement,
        Rage,
        Summons,

        // TODO ResourceMax
        LifeMax,
        ManaMax,
        MovementMax,
        RageMax,
        SummonsMax,

        Range,
        Speed,

        // 
        GlobalAffinity,
        MeleeAffinity,
        DistanceAffinity,
        HealAffinity,
        TrapAffinity,
        GlyphAffinity,
        PoisonAffinity,

        GlobalResistance,
        MeleeResistance,
        DistanceResistance,
        HealResistance,
        TrapResistance,
        GlyphResistance,
        PoisonResistance,

        // elemental
        FireAffinity,
        WaterAffinity,
        AirAffinity,
        EarthAffinity,

        FireResistance,
        WaterResistance,
        AirResistance,
        EarthResistance,


    }
}
