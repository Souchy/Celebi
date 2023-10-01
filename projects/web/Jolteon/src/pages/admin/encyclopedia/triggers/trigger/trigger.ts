import { bindable } from "aurelia";
import { ITriggerModel } from "../../../../../jolteon/services/api/data-contracts";


export class Trigger {

    @bindable
    public model: ITriggerModel;
    @bindable
    public callbacksave: () => {}


    constructor() {

    }

    public save() {
        this.callbacksave(); //this.model);
    }


}
