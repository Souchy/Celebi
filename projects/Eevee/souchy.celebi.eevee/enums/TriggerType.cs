﻿namespace souchy.celebi.eevee.enums
{
    public enum TriggerType
    {
        // simple MomentType, no parameter
        OnFightStart,
        OnFightEnd,
        OnRoundStart,
        OnRoundEnd,
        OnTurnStart,
        OnTurnEnd,
        OnTurnPass,

        // complex, based on an effect, 
        //OnCreatureSwapIn,
        //OnCreatureSwapOut,
        // complex, could filter on the creature id, the spell id, the 
        OnCreatureSpellCast,
        OnEffect,

        OnCreatureWalkEnterCell,
        OnCreatureWalkExitCell,
        OnCreatureWalkStopCell,

        //CompileStats,
    }

    public enum TriggerOrderType {
        Before,
        Apply,
        After
    }

}