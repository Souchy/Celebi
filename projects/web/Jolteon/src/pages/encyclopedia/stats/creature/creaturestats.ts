import { Stats, IStatSimple, IStatBool } from '../../../../jolteon/services/api/data-contracts';
import { IEventAggregator, bindable, inject } from "aurelia";
import { IRouteableComponent } from "@aurelia/router";
import { StatsModelController } from '../../../../jolteon/services/api/StatsModelController';
import { Characteristics } from '../../../../jolteon/constants';

@inject(IEventAggregator, StatsModelController)
export class CreatureStats implements IRouteableComponent {
    public readonly Characteristics: Characteristics = Characteristics; // static reference

    // input
    @bindable
    public baseuid: string;
    @bindable
    public growthuid: string;

    // db data
    public base: Stats = null
    public growth: Stats = null


    constructor(private readonly ea: IEventAggregator, private readonly statsController: StatsModelController) {
        ea.subscribe("stat:base:change", (s: IStatSimple | IStatBool) => {
            // console.log("change base stat: " + JSON.stringify(s))
            // console.log("on change base stat: " +  JSON.stringify(this.base.dic[s.statId]));
            this.base.dic[s.statId] = s;
            statsController.putStats(this.base.entityUid, this.base);
        });
        ea.subscribe("stat:growth:change", (s: IStatSimple | IStatBool) => {
            // console.log("change growth stat: " + JSON.stringify(s))
            this.growth.dic[s.statId] = s;
            statsController.putStats(this.growth.entityUid, this.growth);
        });
    }
    
    binding() {
        // console.log("base uid: " + this.baseuid)
        this.statsController
            .getStats(this.baseuid)
            .then(res => {
                this.base = res.data
            });
        this.statsController
            .getStats(this.growthuid)
            .then(res => this.growth = res.data);
    }

    
}

