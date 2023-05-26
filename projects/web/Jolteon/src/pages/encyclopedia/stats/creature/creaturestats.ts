import { IEventAggregator, bindable, inject } from "aurelia";
import { IRouteableComponent } from "@aurelia/router";
import { Stats,StatSimple, StatBool, UpdateResult } from '../../../../jolteon/services/api/data-contracts';
import { StatsModelController } from '../../../../jolteon/services/api/StatsModelController';
import { Characteristics } from '../../../../jolteon/constants';
import { HttpResponse } from '../../../../jolteon/services/api/http-client';

@inject(IEventAggregator, StatsModelController)
export class CreatureStats implements IRouteableComponent {
    public readonly Characteristics: Characteristics = Characteristics; // static reference

    // input
    @bindable
    public baseuid: string;
    // @bindable
    // public growthuid: string;

    // db data
    public base: Stats = null
    // public growth: Stats = null


    constructor(private readonly ea: IEventAggregator, private readonly statsController: StatsModelController) {
        ea.subscribe("stat:base:change", (s: StatSimple | StatBool) => {
            // console
            this.postUpdate(this.base, s);
        });
        // ea.subscribe("stat:growth:change", (s: StatSimple | StatBool) => {
        //     this.postUpdate(this.growth, s);
        // });
    }

    binding() {
        this.statsController
            .getStats(this.baseuid)
            .then(res => this.base = res.data);
        // this.statsController
        //     .getStats(this.growthuid)
        //     .then(res => this.growth = res.data);
    }

    private async postUpdate(stats: Stats, stat: StatSimple | StatBool) {
        // console.log("postUpdate: " + stat.statId)
        // console.log(stat);
        const parsed = parseInt(stat.value.toString(), 10);

        let promise: Promise<HttpResponse<UpdateResult, any>>;
        if (isNaN(parsed))
            promise = this.statsController.putBool(stats.entityUid, stat as StatBool);
        else
            promise = this.statsController.putSimple(stats.entityUid, stat as StatSimple);

        try {
            let res = await promise;
            if(res.data.matchedCount > 0) 
                this.ea.publish("operation:saved");
            else 
                this.ea.publish("operation:failed");
        } catch(rej) {
            this.ea.publish("operation:failed");
        }
    }


}

