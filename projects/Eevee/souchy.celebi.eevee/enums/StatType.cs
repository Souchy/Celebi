namespace souchy.celebi.eevee.enums
{

    public enum StatValueType
    {
        Simple,
        Detailed,
        Bool,
        Resource
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
        #region Resources & Limits & Regens
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

        LifeRegen, // -1 = full every turn, 0 = no regen, 1 = 1/turn, 0.25 = 1 per 4 turns // string to parse for different regens
        ManaRegen,
        MovementRegen,
        RageRegen,
        #endregion


        #region Affinities & Resistances
        // elemental
        FireAffinity,
        WaterAffinity,
        AirAffinity,
        EarthAffinity,

        FireResistance,
        WaterResistance,
        AirResistance,
        EarthResistance,

        // others
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
        #endregion

        #region Other
        Range,
        Speed, // Initiative in timeline
        #endregion

        #region States ?
        Visible,
        Ghosted, // phasing // is 30% opacity, unlocks line of sight, but blocks movement

        Flying, // slash Hovering
        Underground,

        Drenched, // wet
        Shocked,
        Hot, // burning
        Grounded, // Muddy

        Rooted, // can't translate (dash/push/pull)
        Gravity, // can't teleport
        Heavy, // carry/throw
        Unmoveable, // everything 

        Carrying,
        Carried,

        Pacifist
        #endregion
    }

    public enum ResourceType
    {
        Life = StatType.Life,
        Mana = StatType.Mana,
        Movement = StatType.Movement,
        Rage = StatType.Rage,
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

}
