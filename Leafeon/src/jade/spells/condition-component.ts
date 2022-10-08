import { bindable, IEventAggregator, inject, observable } from "aurelia";
import { CondditionDataState, Condition, ConditionData, ConditionDataContext, ConditionDataStat, ConditionType } from "../../../../arch/common/condition";
import { watch } from '@aurelia/runtime-html';
import { Effect } from "../../../../arch/common/effects";
import { db } from "../../db";

import { conditionComparatorTypes } from "../../../../arch/common/condition";

@inject(db)
export class ConditionComponent {
    public db: db;

    @bindable
    public condition: Condition;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    bind(bindingContext: Object,overrideContext: Object) {
        console.log("condition bind: " + this.condition)    
    }

    @watch('condition.type')
    public typeChanged() {
        switch (this.condition.type) {
            case ConditionType.passthrough:
                this.condition.data = new ConditionData();
                break;
            case ConditionType.stat:
                this.condition.data = new ConditionDataStat();
                break;
            case ConditionType.state:
                this.condition.data = new CondditionDataState();
                break;
            case ConditionType.context:
                this.condition.data = new ConditionDataContext();
                break;
        }
        console.log("type changed: " + JSON.stringify(this.condition))
        this.save();
    }

    public save() {
        this.ea.publish("spells:save");
    }

}
