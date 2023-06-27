import { bindable } from "aurelia";
import { ITrigger } from "../../../../../jolteon/services/api/data-contracts";


export class Trigger {

    @bindable
    public model: ITrigger;
    @bindable
    public callbacksave: (t) => {}


    constructor() {

    }

    public save() {
        this.callbacksave(this.model);
    }


}
