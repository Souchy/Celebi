import { bindable } from "aurelia";
import { ITriggerModel, SchemaDescription } from "../../../../../jolteon/services/api/data-contracts";
import { Enums, Schemas } from "../../../../../jolteon/constants";


export class Trigger {
    // hook enums
    public readonly Enums: Enums = Enums;

    @bindable
    public model: ITriggerModel;
    @bindable
    public callbacksave: () => {}
    // db data
    public schemas = Schemas.triggers;


    constructor() {
    }

    public binding() {
        // console.log("trigger: ");
        // console.log(this.model);
    }

    public get schema(): SchemaDescription {
        let desc = Schemas.triggers.find(s => s.name == this.model.schema.triggerType.name);
        return desc;
    }

    public save() {
        console.log("trigger save")
        this.callbacksave(); //this.model);
    }

}
