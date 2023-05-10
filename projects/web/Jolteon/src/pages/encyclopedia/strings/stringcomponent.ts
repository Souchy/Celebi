import { I18NType, IStringEntity } from '../../../jolteon/services/api/data-contracts';
import { StringController } from './../../../jolteon/services/api/StringController';
import { bindable } from "aurelia";

export class Stringcomponent {

    // input
    @bindable
    public uid: string;
    @bindable
    public editable: boolean = false;

    // db data
    public entity: IStringEntity;

    constructor(private readonly controller: StringController) {
        console.log("ctor string component: " + this.uid);
    }
    
    binding() { 
        this.controller
            .getString(this.uid, { lang: I18NType.Fr })
            .then(res => this.entity = res.data);
    }

}
