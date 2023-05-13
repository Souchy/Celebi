import { IStatBool, IStatSimple, Stats } from './../../../../jolteon/services/api/data-contracts';
import { IEventAggregator, bindable, inject } from "aurelia";
import { IStat, IStats } from "../../../../jolteon/services/api/data-contracts";

@inject(IEventAggregator)
export class Tablesimple {

    @bindable
    public header: string = "Charac";

    @bindable
    public characs: any[];
    @bindable
    public base: Stats 
    @bindable
    public growth: Stats

    constructor(private readonly ea: IEventAggregator) {
    }

    // binding() {
    //     console.log("table base stats: " + this.base);
    // }
    bound() {
        console.log("table base stats: " + this.base);
    }

    public getBaseStat(id) {
        // if(!this.base) return 666;

        if (this.base?.dic?.hasOwnProperty(id)){
            console.log("something ba: ")
            console.log(this.base);
            return this.base["dic"][id]
        }
        else {
            let stat: IStatSimple = {
                statId: id,
                value: 0
            }
            return stat;
        }
    }
    public getGrowthStat(id) {
        // if(!this.growth) return 1337;

        if (this.growth?.dic?.hasOwnProperty(id)){
            console.log("something gr: " + JSON.stringify(this.growth))
            return this.growth["dic"][id]
        }
        else {
            let stat: IStatSimple = {
                statId: id,
                value: 0
            }
            return stat;
        }
    }



}
