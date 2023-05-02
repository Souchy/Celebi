import { watch } from "@aurelia/runtime-html";
import { bindable, IEventAggregator, inject } from "aurelia";
import { Effect } from "../../../../../../arch/common/effects";
import { db } from "../../../db";

@inject(db)
export class EffectData {

    public db: db;

    @bindable
    public effect: Effect

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    @watch('effect.type')
    public getType() {
        return this.effect.type;
    }
    
    public save() {
        this.ea.publish("spells:save");
    }
}
