
import { bindable, IEventAggregator, inject } from "aurelia";
import { TargetFilter } from "../../../../arch/common/target";
import { db } from "../../db";

@inject(db)
export class TargetFilterComponent {

    @bindable
    public filter: TargetFilter;

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {

    }

    public save(){
        this.ea.publish("spells:save");
    }

}
