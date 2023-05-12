import { IRouteableComponent } from '@aurelia/router';
import { I18NType, IStringEntity } from '../../../jolteon/services/api/data-contracts';
import { StringController } from './../../../jolteon/services/api/StringController';
import { bindable } from "aurelia";

export class Stringcomponent implements IRouteableComponent {

    // input
    @bindable
    public uid: string;
    @bindable
    public editable: boolean = false;

    // db data
    public entity: IStringEntity;

    constructor(private readonly controller: StringController) {
    }
    
    binding() { 
        this.controller
            .getString(this.uid, { lang: I18NType.fr })
            .then(res => this.entity = res.data);
    }

}
