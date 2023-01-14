import { watch } from "@aurelia/runtime-html";
import { bindable, IEventAggregator, inject } from "aurelia";
import { Zone as ZoneData } from "../../../../../arch/common/components/zone";
import { Effect } from "../../../../../arch/common/effects";
import { db } from "../../db";

@inject(db)
export class Zone {

    public db: db;

    @bindable
    public zone: ZoneData

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    // @watch('effect.type')
    // public getType() {
    //     return this.effect.type;
    // }
    
    public save() {
        this.ea.publish("spells:save");
    }
}
