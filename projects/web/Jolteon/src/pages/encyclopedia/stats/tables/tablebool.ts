import { IEventAggregator, bindable, inject } from "aurelia";
import { IStat, IStats, StatBool, StatSimple, Stats } from "../../../../jolteon/services/api/data-contracts";


@inject(IEventAggregator)
export class Tablebool {

    // @bindable
    // public header: string = "Charac";

    @bindable
    public characs: any[];
    @bindable
    public base: Stats 
    // @bindable
    // public growth: Stats

    constructor(private readonly ea: IEventAggregator) {
    }

    // binding() {
    //     console.log("table base stats: " + this.base);
    // }
    bound() {
        // console.log("table base stats: " + this.base);
    }

    public getBaseStat(id): StatBool {
        if (this.base?.dic?.hasOwnProperty(id)){
            return this.base.dic[id]
        }
        else {
            let stat: StatBool = {
                statId: id,
                value: false
            }
            return stat;
        }
    }

}
