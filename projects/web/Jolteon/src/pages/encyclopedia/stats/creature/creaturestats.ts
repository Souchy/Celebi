import { IStats, AffinityTypes, ResourceTypes, StateTypes, ResistanceTypes, ContextualTypes, OtherPropertyTypes } from '../../../../jolteon/services/api/data-contracts';
import { bindable } from "aurelia";
import { IRouteableComponent } from "@aurelia/router";
import { StatsModelController } from '../../../../jolteon/services/api/StatsModelController';

export class CreatureStats implements IRouteableComponent {

    // input
    @bindable
    public baseuid: string;
    @bindable
    public growthuid: string;

    // db data
    public base: any = {}// = { dic: {}};
    public growth: any = {} //= { dic: {}};

    // creature properties
    private affinities = Object.values(AffinityTypes);
    private resistances = Object.values(ResistanceTypes);
    private resources = Object.values(ResourceTypes);
    private states = Object.values(StateTypes);
    private others = Object.values(OtherPropertyTypes);
    private contextuals = Object.values(ContextualTypes);

    constructor(private readonly statsController: StatsModelController) {
    }
    
    binding() {
        // console.log("base uid: " + this.baseuid)
        this.statsController
            .getStats(this.baseuid)
            .then(res => {
                this.base = res.data
                console.log("creature base stats: " + this.base["dic"][ResourceTypes.Life.id].value);
                // console.log(this.base)
            });
        this.statsController
            .getStats(this.growthuid)
            .then(res => this.growth = res.data);
    }

    public onInputChange() {
        console.log("onInputChange: " + JSON.stringify("{}"));
    }
}

