import { IRouteableComponent } from '@aurelia/router';
import { I18NType, IStringEntity, StringEntity } from '../../../jolteon/services/api/data-contracts';
import { StringController } from './../../../jolteon/services/api/StringController';
import { IEventAggregator, bindable, containerless, inject } from "aurelia";

@inject(IEventAggregator, StringController)
// @containerless
export class Stringcomponent implements IRouteableComponent {

    // input
    @bindable
    public uid: string;
    @bindable
    public editable: boolean = false;
    @bindable
    public lang: I18NType = I18NType.fr;
    @bindable
    public large: boolean = false;

    // db data
    private entity: StringEntity;

    constructor(private readonly ea: IEventAggregator, private readonly controller: StringController) {
    }
    
    binding() { 
        this.controller
            .getString(this.uid, { lang: this.lang })
            .then(res => this.entity = res.data);
    }

    public onChange() {
        this.postUpdate(this.entity);
    }
    private async postUpdate(str: StringEntity) {
        // console.log("postUpdate: " + str)
        // console.log(stat);
        try {
            let res = await this.controller.putString(str.entityUid, str, { lang: this.lang });
            if(res.data.matchedCount > 0) 
                this.ea.publish("operation:saved");
            else 
                this.ea.publish("operation:failed");
        } catch(rej) {
            this.ea.publish("operation:failed");
        }
    }


}
