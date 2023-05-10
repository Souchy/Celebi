import { bindable } from "aurelia";
import { StatsModelController } from "../../../jolteon/services/api/StatsModelController";
import { IStats } from "../../../jolteon/services/api/data-contracts";
import { IRouteableComponent } from "@aurelia/router";

export class Statscomponent implements IRouteableComponent {

    // input
    @bindable
    public uid: string;

    // db data
    public stats: IStats;

    constructor(
        private readonly statsController: StatsModelController
    ) {
    }

    binding() { //initiator: IHydratedController, parent: IHydratedController, flags: LifecycleFlags) {
        this.statsController
            .getStats(this.uid)
            .then(res => this.stats = res.data);
    }

}
