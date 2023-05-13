import { ICreatureModel } from "../../../jolteon/services/api/data-contracts";
import { IEventAggregator, bindable, inject } from "aurelia";
import { CreatureModelController } from "../../../jolteon/services/api/CreatureModelController";
import { IRouteableComponent, IRouter, Navigation, Parameters, RoutingInstruction } from '@aurelia/router';
import { StatsType } from "../stats/statscomponent";
import { TOAST_PLACEMENT, TOAST_STATUS, TOAST_THEME, Toast, ToastConfigOptions, ToastOptions } from "bootstrap-toaster";


@inject(CreatureModelController, IEventAggregator, IRouter)
export class Creature implements IRouteableComponent {

    private readonly toastConfig: ToastConfigOptions = {
        enableQueue: false,
        placement: TOAST_PLACEMENT.TOP_CENTER,
        maxToasts: 2
    }

    //#region input
    @bindable
    public model: ICreatureModel;
    @bindable
    public isvignette: boolean = false;
    /** Creature modelID from Route: https://celebi.com/creatures/{id} */
    public uid: string;
    //#endregion

    constructor(
        private readonly creatureController: CreatureModelController,
        private readonly ea: IEventAggregator, 
        private readonly router: IRouter
    ) {
        ea.subscribe("creature:operation:saved", this.toastSaved);
        ea.subscribe("creature:operation:failed", this.toastFailed);
        Toast.configure(this.toastConfig);
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
        this.uid = parameters["uid"] as string;
        // console.log("creature loading: " + this.uid)
        try {
            let res = await this.creatureController.getCreature(this.uid)
            this.model = res.data;
            // console.log("creature loading: " + JSON.stringify(this.model))
        } catch (rej) {
            this.router.load("editor");
        }
    }

    private toastSaved() {
        // console.log("toast saved")
        let toast: ToastOptions = {
            title: "Creature",
            message: "Saved",
            status: TOAST_STATUS.SUCCESS,
            timeout: 2000
        }
        // Toast.configure(this.toastConfig);
        Toast.create(toast);
    }
    private toastFailed() {
        // console.log("toast failed")
        let toast = {
            title: "Creature",
            message: "Failed",
            status: TOAST_STATUS.WARNING,
            timeout: 2000
        }
        // Toast.configure(this.toastConfig);
        Toast.create(toast);
    }


}
