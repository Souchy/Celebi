import { bindable } from "aurelia";
import { ICondition } from "../../../../jolteon/services/api/data-contracts";

export class Condition {

    @bindable
    public model: ICondition;

    @bindable
    public depth: number = 0;

    constructor() {
        
    }

}

/*
Conditions:
    {
        // usable as a child of a regular filter, to filter creature stats
        // or a child of status filter, which then it filters status stats :O 
        // StatsCondition:
        //      StatSimpleFilter
        //      StatBoolFilter
        stats: [
            {
                stat: statId
                valueMin: x
                valueMax: y 
            },
            {
                boolstat: statId,
                value: true // (no max for bool types)
            }
        ]
    },
    {
        boardTargetType: x
        statusSourceTeam: x,
        statusRequired: [...],
        statusRejected: [...],
    },
    {
        creatureModelRequired: [...],
        creatureModelRejected: [...]
    },
    {
        teamRequired: [...],
        teamRejected: [...]
    },
    {
        allowSummon: true,
        allowSummoner: true
    },
    {
        distance: x
    },
    {
        lineOfSight: true
    },
*/
