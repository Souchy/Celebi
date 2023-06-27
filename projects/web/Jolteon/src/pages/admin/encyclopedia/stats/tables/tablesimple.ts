import { IEventAggregator, bindable, inject } from "aurelia";
import { MathEquation, StatSimple, Stats } from '../../../../../jolteon/services/api/data-contracts';
import { IStat, IStats } from "../../../../../jolteon/services/api/data-contracts";
import { Constants } from "../../../../../jolteon/constants";

@inject(IEventAggregator)
export class Tablesimple {

    @bindable
    public hasheader: boolean = true;

    @bindable
    public characs: any[];
    @bindable
    public base: Stats = null
    // @bindable
    // public growth: Stats = null

    constructor(private readonly ea: IEventAggregator) {
    }

    bound() {
        // console.log("table base stats: " + this.base);
    }

    public getBaseStat(id): StatSimple {
        if (this.base?.base?.hasOwnProperty(id)){
            return this.base.base[id]
        }
        else {
            let stat: StatSimple = {
                statId: id,
                value: 0
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
