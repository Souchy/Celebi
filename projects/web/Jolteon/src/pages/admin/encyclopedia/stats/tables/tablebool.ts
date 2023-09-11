import { IEventAggregator, bindable, inject } from "aurelia";
import { MathEquation, StatBool, Stats } from "../../../../../jolteon/services/api/data-contracts";
import { Constants } from "../../../../../jolteon/constants";


@inject(IEventAggregator)
export class Tablebool {

    // @bindable
    // public header: string = "Charac";

    @bindable
    public characs: any[];
    @bindable
    public stats: Stats 
    // @bindable
    // public growth: Stats

    constructor(private readonly ea: IEventAggregator) {
    }

    bound() {
        // console.log("table base stats: " + this.base);
    }

    public getBaseStat(id): StatBool {
        if (this.stats?.base?.dic?.hasOwnProperty(id)){
            return this.stats.base.dic[id]
        }
        else {
            let stat: StatBool = {
                statId: id,
                value: false
            }
            return stat;
        }
    }
    public getGrowthEquation(id): MathEquation {
        if (this.stats?.growth?.dic?.hasOwnProperty(id)){
            return this.stats.growth.dic[id]
        }
        else {
            let equation: MathEquation = {
                functions: [
                    {
                        xFromIncluded: Constants.MAX_INT,
                        xToExcluded: Constants.MIN_INT,
                        slopes: [0]
                    }
                ]
            }
            return equation;
        }
    }

}
