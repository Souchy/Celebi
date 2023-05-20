import { SchemaDescription } from './../../../jolteon/services/api/data-contracts';
import { IRouter } from "@aurelia/router";
import { IEventAggregator, bindable, inject } from "aurelia";
import { EffT, IEffect } from "../../../jolteon/services/api/data-contracts";
import { EffectPermanentController } from "../../../jolteon/services/api/EffectPermanentController";
import { PropertiesController } from "../../../jolteon/services/api/PropertiesController";

@inject(IEventAggregator, IRouter)
export class Effect {

    @bindable
    public uid: string;

    // db data
    private model: IEffect;
    // db data
    private schema: SchemaDescription

    constructor(
        private readonly ea: IEventAggregator, 
        private readonly router: IRouter, 
        private readonly effectController: EffectPermanentController,
        private readonly propertiesController: PropertiesController
    ) {
    }

    binding() {
        this.effectController.getEffect(this.uid).then(
            res => {
                this.model = res.data;
                console.log("binded effect: ") // + JSON.stringify(this.effect))
                console.log(this.model);
                this.propertiesController.getEffectsSchema(this.getModelName())
                    .then(res => this.schema = res.data);
            },
            rej => {
                console.log(rej);
            }
        )
    }

    public getModelName() {
        let id: number = +this.model.modelUid;
        let enu = EffT[id]; //id as EffT;
        console.log("enu: " + enu.toString())
        return enu.toString();
        // let key = Object.keys(EffT)[id];

    }

}
