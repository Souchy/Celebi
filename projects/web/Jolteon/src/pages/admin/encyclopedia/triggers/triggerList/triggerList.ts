import { bindable } from "aurelia";
import { IEffect, ITriggerModel, SchemaDescription } from "../../../../../jolteon/services/api/data-contracts";
import { TriggerModelController } from "../../../../../jolteon/services/api/TriggerModelController";
import { Schemas } from "../../../../../jolteon/constants";


export class TriggerList {

    @bindable
    public callbacksave: () => {}

    // db data
    @bindable
    public effect: IEffect;
    // db data
    public schemas = Schemas.triggers;

    constructor(private readonly triggerController: TriggerModelController) {
    }

    public get modaluid() {
        return this.effect.entityUid;
    }

    binding() {
        // this.printList();
    }

    public printList() {
        console.log("Triggers for effect " + this.effect.entityUid + " : ");
        console.log(this.effect.triggers);
    }

    public onAddTrigger(schema: SchemaDescription) {
        console.log("On add trigger to effect " + this.effect.entityUid)
        // console.log({schema});
        this.triggerController.postNew({ schemaName: schema.name })
            .then(res => this.effect.triggers.push(res.data))
            .then(f => this.callbacksave());
    }

    public clearList() {
        this.effect.triggers = [];
        this.callbacksave();
    }

    public save() {
        this.callbacksave;
    }

}
