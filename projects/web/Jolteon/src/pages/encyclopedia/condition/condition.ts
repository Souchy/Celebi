import { ICondition } from "../../../jolteon/services/api/data-contracts";

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
export class Condition {

    public model: ICondition;

    constructor() {
        
    }

}
