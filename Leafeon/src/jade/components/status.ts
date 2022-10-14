import { bindable, IEventAggregator, inject } from "aurelia";
import { db } from "../../db";
import { Status as StatusObject, StatusInstance } from "../../../../arch/common/red/status";


@inject(db)
export class Status {

    private static counter = 0;
    public readonly uid = Status.counter++;

    public db: db;

    // @bindable
    

    constructor(db: db, @IEventAggregator readonly ea: IEventAggregator) {
        this.db = db;
    }

    public save(){
        this.ea.publish("spells:save");
    }

}
