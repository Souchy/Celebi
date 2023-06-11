import { IEventAggregator, bindable } from "aurelia";
import { IStatusModel } from "../../../jolteon/services/api/data-contracts";
import { IRouter, Navigation, RoutingInstruction, Parameters } from "@aurelia/router";
import { StatusModelController } from "../../../jolteon/services/api/StatusModelController";


export class Status {
    
    @bindable
    public mode: string = 'root';
    @bindable
    public isvignette: boolean = false;
    @bindable
    public callbackremove = (status: IStatusModel) => { };
    @bindable
    public callbackadd = (status: IStatusModel) => { };

    // db data from route loading
    public model: IStatusModel


    constructor(
        private readonly ea: IEventAggregator,
        private readonly router: IRouter,
        private readonly statusController: StatusModelController,
    ) {
    }

    /**
     * Hook to attribute binding
     */
    binding() {
        // console.log("creature binding: " + this.uid + ", " + JSON.stringify(this.model))
    }
    /**
     * Hook to route loading
     */
    async loading?(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): Promise<void> {
        let uid = parameters["uid"] as string;
        // console.log("creature loading: " + this.uid)
        try {
            let res = await this.statusController.getStatus(uid);
            this.model = res.data;
            console.log("nav to new creature")
            this.ea.publish("navcrumb:spell", null)
            this.ea.publish("navcrumb:creature", null)
            this.ea.publish("navcrumb:status", {
                modeluid: this.model.modelUid,
                nameuid: this.model.nameId
            })
            // console.log("creature loading: " + JSON.stringify(this.model))
        } catch (rej) {
            this.router.load("editor");
        }
    }

    public clickStatus() {
        // console.log("click spell: " + this.mode)
        if (this.mode == 'root' || this.mode == 'creature') {
            // console.log("click spell " + modelUid)
            this.router.load("/editor/spell/" + this.model.modelUid);
        }
        if (this.mode == 'search') {
            // console.log("spell search click: " + this.model.modelUid)
            this.callbackadd(this.model);
        }
    }
    public clickRemove() {

    }
    public onChange() {

    }
    // public onMoveEffectUp() {

    // }
    // public onMoveEffectDown() {

    // }
    // public onRemoveEffect() {

    // }

}
