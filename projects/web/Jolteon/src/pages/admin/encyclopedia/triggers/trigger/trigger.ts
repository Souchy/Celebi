import { bindable } from "aurelia";
import { ITriggerModel, SchemaDescription } from "../../../../../jolteon/services/api/data-contracts";
import { Schemas } from "../../../../../jolteon/constants";


export class Trigger {

    @bindable
    public model: ITriggerModel;
    @bindable
    public callbacksave: () => {}


    constructor() {
    }

    public binding() {
        console.log("trigger: ");
        console.log(this.model);
    }

    public get schema(): SchemaDescription {
        let desc = Schemas.triggers.find(s => s.name == this.model.schema.triggerType.name);
        return desc;
    }

    public save() {
        this.callbacksave(); //this.model);
    }

}
