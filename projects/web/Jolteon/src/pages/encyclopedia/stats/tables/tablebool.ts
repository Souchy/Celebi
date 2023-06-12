import { IEventAggregator, bindable, inject } from "aurelia";
import { MathEquation, StatBool, Stats } from "../../../../jolteon/services/api/data-contracts";
import { Constants } from "../../../../jolteon/constants";


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

    bound() {
        // console.log("table base stats: " + this.base);
    }

    public getBaseStat(id): StatBool {
        if (this.base?.base?.hasOwnProperty(id)){
            return this.base.base[id]
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
        if (this.base?.growth?.hasOwnProperty(id)){
            return this.base.growth[id]
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
