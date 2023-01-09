import { bindable, IEventAggregator, inject, observable } from "aurelia";
import { Condition as ConditionObject, ConditionData, CondditionDataState, ConditionDataContext, ConditionDataStat, ConditionType, conditionComparatorTypes } from "../../../../../arch/common/components/condition";
import { watch } from '@aurelia/runtime-html';
import { Effect } from "../../../../../arch/common/effects";
import { db } from "../../../db";

@inject(db)
export class ConditionComponent {
    public db: db;

    @bindable // either condition or effect
    public parent: any; //Condition|Effect;
    @bindable
    public condition: ConditionObject;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    bind(bindingContext: Object, overrideContext: Object) {
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
        // console.log("type changed: " + JSON.stringify(this.condition))
        this.save();
    }

    public addChild() {
        let cond = new ConditionObject();
        cond.parent = this.condition;
        this.condition.children.push(cond);
        this.save();
    }

    // public deleteChild(child) {
    //     let index = this.condition.children.indexOf(child);
    //     this.condition.children.splice(index, 1);
    //     this.save();
    // }
    public deleteSelf() {
        if (this.parent.isCondition) {
            let papa = this.parent as ConditionObject;
            let index = papa.children.indexOf(this.condition);
            papa.children.splice(index, 1);
            this.save();
        } 
        if (this.parent.isEffect) {
            this.condition.type = ConditionType.passthrough;
            this.condition.data = {};
            this.condition.children = [];
            this.save();
        }
    }

    public save() {
        this.ea.publish("spells:save");
    }

    @watch('condition')
    public getDepth() {
        let depth = 0;
        let obj = this.condition;
        while ((obj = obj.parent) != null) {
            depth++
        }
        return depth;
    }

    public translateComparator(type) {
        switch (type) {
            case "lt": return "<";
            case "le": return "<=";
            case "e": return "=";
            case "ge": return ">=";
            case "gt": return ">";
        }
        return type;
    }

}
