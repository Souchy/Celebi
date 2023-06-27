import { IEventAggregator, bindable, inject } from "aurelia";
import { IStatusModel } from "../../../../jolteon/services/api/data-contracts";
import { IRouter, Navigation, RoutingInstruction, Parameters } from "@aurelia/router";
import { StatusModelController } from "../../../../jolteon/services/api/StatusModelController";
import { Characteristics } from "../../../../jolteon/constants";

@inject(IEventAggregator, IRouter, StatusModelController)
export class Status {

    public Characteristics: Characteristics = Characteristics;
    
    @bindable
    public mode: string = 'root';
    @bindable
    public isvignette: boolean = false;
    @bindable
    public callbackremove = (status: IStatusModel) => { };
    @bindable
    public callbackadd = (status: IStatusModel) => { };

    // db data from route loading or from status list
    @bindable
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
        // console.log("status binding: " + this.model + ", " + JSON.stringify(this.model))
    }
    /**
     * Hook to route loading
     */
    async loading?(parameters: Parameters, instruction: RoutingInstruction, navigation: Navigation): Promise<void> {
        let uid = parameters["uid"] as string;
        // console.log("status loading: " + uid)
        try {
            let res = await this.statusController.getStatus(uid);
            this.model = res.data;
            console.log("nav to new status")
            this.ea.publish("navcrumb:spell", null)
            this.ea.publish("navcrumb:creature", null)
            this.ea.publish("navcrumb:status", {
                modeluid: this.model.modelUid,
                nameuid: this.model.nameId
            })
            // console.log("status loading: " + JSON.stringify(this.model))
        } catch (rej) {
            this.router.load("editor");
        }
    }

    public clickStatus() {
        console.log("click status: " + this.mode)
        if (this.mode == 'root' || this.mode == 'creature') {
            console.log("click status " + this.model.entityUid)
            this.router.load("/editor/status/" + this.model.entityUid);
        }
        if (this.mode == 'search') {
            // console.log("status search click: " + this.model.modelUid)
            this.callbackadd(this.model);
        }
    }
    public clickRemove() {
        console.log("status click remove")
        this.callbackremove(this.model);
    }
    public onSave() {
        // console.log("status on change")
        this.statusController.putStatus(this.model.entityUid, this.model)
            .then(res => this.model = res.data)
            .then(f => this.ea.publish("operation:saved"))
    }
    // public onMoveEffectUp() {

    // }
    // public onMoveEffectDown() {

    // }
    // public onRemoveEffect() {

    // }

}
