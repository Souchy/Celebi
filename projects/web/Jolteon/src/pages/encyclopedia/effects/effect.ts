import { IRouter } from "@aurelia/router";
import { IEventAggregator, bindable, inject } from "aurelia";
import { EffectModelController } from "../../../jolteon/services/api/EffectModelController";
import { IEffect } from "../../../jolteon/services/api/data-contracts";

@inject(IEventAggregator, IRouter)
export class Effect {

    @bindable
    public uid: string;

    // db data
    private effect: IEffect;

    constructor(
        private readonly ea: IEventAggregator, 
        private readonly router: IRouter, 
        private readonly effectController: EffectModelController
    ) {
    }

    binding() {
        this.effectController.getEffect(this.uid).then(
            res => {
                this.effect = res.data;
                // this.effect.
            },
            rej => {
                console.log(rej);
            }
        )
    }


}
