import { IStatBool, IStatSimple, StatSimple, Stats } from './../../../../jolteon/services/api/data-contracts';
import { IEventAggregator, bindable, inject } from "aurelia";
import { IStat, IStats } from "../../../../jolteon/services/api/data-contracts";

@inject(IEventAggregator)
export class Tablesimple {

    // @bindable
    // public header: string = "Charac";
    @bindable
    public hasheader: boolean = true;

    @bindable
    public characs: any[];
    @bindable
    public base: Stats = null
    @bindable
    public growth: Stats = null

    constructor(private readonly ea: IEventAggregator) {
    }

    // binding() {
    //     console.log("table base stats: " + this.base);
    // }
    bound() {
        // console.log("table base stats: " + this.base);
    }

    public getBaseStat(id): StatSimple {
        if (this.base?.dic?.hasOwnProperty(id)){
            return this.base.dic[id]
        }
        else {
            let stat: StatSimple = {
                statId: id,
                value: 0
            }
            return stat;
        }
    }
    public getGrowthStat(id): StatSimple {
        if(!this.growth) return null;
        if (this.growth?.dic?.hasOwnProperty(id)){
            return this.growth.dic[id]
        }
        else {
            let stat: StatSimple = {
                statId: id,
                value: 0
            }
            return stat;
        }
    }



}
