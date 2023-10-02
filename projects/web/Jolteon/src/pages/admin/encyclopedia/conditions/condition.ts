import { Bindable, bindable } from "aurelia";
import { ICondition, SchemaDescription } from "../../../../jolteon/services/api/data-contracts";
import { ConditionModelController } from "../../../../jolteon/services/api/ConditionModelController";
import { Schemas } from "../../../../jolteon/constants";

export class Condition {

    @bindable
    public parent: any;
    @bindable
    public parentPropertyName: string;

    @bindable
    public model: ICondition;

    // @bindable
    // public depth: number = 0;

    @bindable
    public callbacksave = () => {};

    // db data
    public schemas = Schemas.conditions;

    constructor(private readonly conditionController: ConditionModelController) {
    }

    public get modelName() {
        return this.model.conditionType.name;
    }
    public get schema(): SchemaDescription {
        let desc = Schemas.conditions.find(s => s.name == this.modelName);
        return desc;
    }

    public create(e: SchemaDescription) {
        this.conditionController.postNew({ schemaName: e.name })
            .then(res => this.model = res.data)
            .then(f => this.parent[this.parentPropertyName] = this.model)
            .then(f => this.save());
    }

    public save() {
        this.callbacksave();
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
