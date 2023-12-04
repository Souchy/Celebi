import { bindable } from "aurelia";
import { ITriggerModel, SchemaDescription } from "../../../../../jolteon/services/api/data-contracts";
import { Enums, Schemas } from "../../../../../jolteon/constants";
import { TriggerModelController } from "../../../../../jolteon/services/api/TriggerModelController";


export class Trigger {
    // hook enums
    public readonly Enums: Enums = Enums;

    @bindable
    public model: ITriggerModel;
    @bindable
    public callbacksave: () => {}
    @bindable
    public callbackdelete: () => {}
    @bindable
    public callbackchange: (e) => {}
    // db data
    public schemas = Schemas.triggers;


    constructor(private readonly triggerController: TriggerModelController) {
    }

    public binding() {
        // console.log("trigger: ");
        // console.log(this.model);
    }

    public get schema(): SchemaDescription {
        let desc = Schemas.triggers.find(s => s.name == this.model.schema.triggerType.name);
        return desc;
    }

    public onChangeSchemaType(e: SchemaDescription) {
        this.callbackchange(e);
    }

    public delete() {
        this.callbackdelete();
    }

    public save() {
        console.log("trigger save")
        this.callbacksave(); //this.model);
    }

}
