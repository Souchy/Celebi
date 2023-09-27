
import { bindable, IEventAggregator, inject } from "aurelia";
import { TargetFilter as TargetFilterData } from "../../../../../../arch/common/components/target";
import { db } from "../../db";

@inject(db)
export class TargetFilter {

    private static counter = 0;
    public readonly uid = TargetFilter.counter++;

    public db: db;
    
    @bindable
    public filter: TargetFilterData;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    public save(){
        this.ea.publish("spells:save");
    }

}
